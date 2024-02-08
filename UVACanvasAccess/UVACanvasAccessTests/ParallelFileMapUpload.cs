using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using dotenv.net;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Util;
using Xunit;
using Xunit.Abstractions;

namespace UVACanvasAccessTests
{
    public class ParallelFileMapUpload
    {
        private readonly Api _api;
        private readonly ITestOutputHelper _testOutputHelper;

        public ParallelFileMapUpload(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            DotEnv.Load();
            _api = new Api(Environment.GetEnvironmentVariable("TEST_TOKEN")
                ?? throw new ArgumentException(".env should contain TEST_TOKEN"),
                "https://uview.instructure.com/api/v1/");
        }

        [Fact]
        public async Task Run()
        {
            var fileMapDir = Environment.GetEnvironmentVariable("TEST_MAP_DIR")
                ?? throw new ArgumentException(".env should contain TEST_MAP_DIR");
            if (!fileMapDir.EndsWith(Path.DirectorySeparatorChar.ToString())) fileMapDir += Path.PathSeparator;

            var list = File.ReadAllLines(fileMapDir + "map.csv").ToList();

            // split the tasks into chunks so we have about as many chunks as logical processors.
            var taskLists = list.Chunk(list.Count / Environment.ProcessorCount)
                .ToArray();

            var nThreads = taskLists.Length;

            // every thread needs ownership of a single Api instance because masquerading is not thread safe.
            var apis = new Api[nThreads];

            var token = Environment.GetEnvironmentVariable("TEST_TOKEN")
                ?? throw new ArgumentException(".env should contain TEST_TOKEN");

            for (var i = 0; i < nThreads; i++)
            {
                apis[i] = new Api(token, "https://uview.instructure.com/api/v1/");
            }

            _testOutputHelper.WriteLine($"Using {nThreads} threads.");

            // keep track of the file ids successfully uploaded so we can verify at the end that all tasks ran.
            var completed = new ConcurrentBag<ulong>();

            using (var countdown = new CountdownEvent(nThreads))
            {
                for (var i = 0; i < nThreads; i++)
                {
                    ThreadPool.QueueUserWorkItem(o =>
                    {
                        try
                        {
                            var n = (int) o;
                            foreach (var line in taskLists[n])
                            {
                                var halves = line.Split(',');
                                Debug.Assert(halves.Length == 2);

                                var (userKey, userFile) = (halves[0], halves[1]);

                                var api = apis[n];

                                try
                                {
                                    var user = api.StreamUsers(userKey)
                                        .FirstOrDefaultAsync(u => u.SisUserId == userKey)
                                        .Result;

                                    if (user == null)
                                    {
                                        _testOutputHelper.WriteLine(
                                            $"WARN: Couldn't find the user for sis {userKey} !!");
                                        continue;
                                    }

                                    var bytes = File.ReadAllBytes(fileMapDir + userFile);

                                    _testOutputHelper.WriteLine($"Preparing to upload filename {userFile} to user " +
                                        $"{userKey}, Id {user.Id}, SIS {user.SisUserId}");

                                    api.MasqueradeAs(user.Id);

                                    var file = api.UploadPersonalFile(bytes,
                                            userFile,
                                            "test_csv_file_upload_p3")
                                        .Result;

                                    _testOutputHelper.WriteLine($"Uploaded as {file.Id}!");
                                    completed.Add(file.Id);
                                }
                                finally
                                {
                                    api.StopMasquerading();
                                }
                            }
                        }
                        finally
                        {
                            countdown.Signal();
                        }
                    }, i);
                }
                countdown.Wait();
            }

            _testOutputHelper.WriteLine(
                $"{completed.Distinct().Count()} out of {list.Count} operations were completed.");
        }
    }
}