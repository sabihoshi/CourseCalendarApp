using System;
using System.Linq;
using System.Threading.Tasks;
using dotenv.net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Util;
using Xunit;
using Xunit.Abstractions;
using static UVACanvasAccess.ApiParts.Api.CourseEnrollmentType;

namespace UVACanvasAccessTests
{
    public class CurrentAssignmentsReportTabularized
    {
        private readonly Api _api;
        private readonly ITestOutputHelper _testOutputHelper;

        public CurrentAssignmentsReportTabularized(ITestOutputHelper testOutputHelper)
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
            var sample = _api.StreamUsers()
                .Where(u => !u.Name.ToLowerInvariant().Contains("test"))
                .Where(u => !u.SisUserId?.StartsWith("pG") ?? false)
                .Where(u => !u.IsTeacher().Result)
                .Take(10); // LIMITS number of users reported, comment this for full report

            uint usersInReport = 0;
            var usersObj = new JObject();
            var coursesObj = new JObject();
            var countsObj = new JObject();

            var document = new JObject
            {
                ["dateGenerated"] = DateTime.Now.ToIso8601Date(),
                ["users"]         = usersObj,
                ["courses"]       = coursesObj,
                ["counts"]        = countsObj
            };

            await foreach (var user in sample)
            {
                usersObj[user.Id.ToString()] = new JObject
                {
                    ["userSis"]      = user.SisUserId,
                    ["userFullName"] = user.Name
                };

                _api.MasqueradeAs(user.Id);

                await foreach (var userEnrollment in _api.StreamUserEnrollments(user.Id, new[] { StudentEnrollment }))
                {
                    var currentAssignments = await _api.StreamCourseAssignments(userEnrollment.CourseId)
                        .Where(a => a.Published)
                        .Where(a => a.DueAt != null && a.DueAt.Value > DateTime.Now)
                        .Where(a => !a.LockedForUser)
                        .ToListAsync();

                    var completedAssignments = currentAssignments.Count(a => a.Submission != null);

                    coursesObj[userEnrollment.CourseId.ToString()] = JToken.FromObject(new
                    {
                        courseSisId = userEnrollment.SisCourseId,
                        courseName  = await _api.GetCourse(userEnrollment.CourseId).ThenApply(c => c.Name)
                    });

                    if (countsObj[user.Id.ToString()] == null) countsObj[user.Id.ToString()] = new JObject();
                    countsObj[user.Id.ToString()][userEnrollment.CourseId.ToString()] = JToken.FromObject(new
                    {
                        completeCurrentAssignments   = completedAssignments,
                        incompleteCurrentAssignments = currentAssignments.Count - completedAssignments
                    });
                }

                _api.StopMasquerading();

                ++usersInReport;
            }

            document["usersInReport"] = usersInReport;

            _testOutputHelper.WriteLine(document.ToString(Formatting.Indented));
        }
    }
}