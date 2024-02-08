using System;
using dotenv.net;
using UVACanvasAccess.ApiParts;

namespace UVACanvasAccessTests
{
    // ReSharper disable MemberCanBePrivate.Global
    public class ApiFixture : IDisposable
    {
        public ApiFixture()
        {
            DotEnv.Load();

            Api = new Api(Environment.GetEnvironmentVariable("TEST_TOKEN"),
                "https://uview.instructure.com/api/v1/");
        }

        public Api Api { get; }

        public void Dispose() { Api.Dispose(); }
    }
}