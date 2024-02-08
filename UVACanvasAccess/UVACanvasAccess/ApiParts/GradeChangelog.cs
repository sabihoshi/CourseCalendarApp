using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UVACanvasAccess.Model.GradeChangelog;
using UVACanvasAccess.Structures.GradeChangelog;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.ApiParts
{
    public partial class Api
    {
        public async Task<IEnumerable<GradeChangeEvent>> GetAssignmentGradeChangelog(ulong assignmentId,
            DateTime? start = null,
            DateTime? end = null)
        {
            var response = await RawGetAssignmentGradeChangelog(assignmentId.ToString(), start, end);

            // the docs for this endpoint are lies. instead of returning a paginated list of GradeChangeEvent, it
            // returns a single object that:
            // 1. may or may not be paginated
            // 2. if it is paginated, won't work with normal pagination, because it's an object with lists in it
            // 3. is undocumented
            // 4. contains extra, redundant data
            var redundant =
                JsonConvert.DeserializeObject<RedundantGradeChangeEventResponse>(
                    await response.Content.ReadAsStringAsync());

            return from model in redundant.Events
                select new GradeChangeEvent(this, model);
        }

        public async Task<IEnumerable<GradeChangeEvent>> GetCourseGradeChangelog(ulong courseId,
            DateTime? start = null,
            DateTime? end = null)
        {
            var response = await RawGetCourseGradeChangelog(courseId.ToString(), start, end);

            var redundant =
                JsonConvert.DeserializeObject<RedundantGradeChangeEventResponse>(
                    await response.Content.ReadAsStringAsync());

            return from model in redundant.Events
                select new GradeChangeEvent(this, model);
        }

        public async Task<IEnumerable<GradeChangeEvent>> GetGraderGradeChangelog(ulong graderId,
            DateTime? start = null,
            DateTime? end = null)
        {
            var response = await RawGetGraderGradeChangelog(graderId.ToString(), start, end);

            var redundant =
                JsonConvert.DeserializeObject<RedundantGradeChangeEventResponse>(
                    await response.Content.ReadAsStringAsync());

            return from model in redundant.Events
                select new GradeChangeEvent(this, model);
        }

        public async Task<IEnumerable<GradeChangeEvent>> GetStudentGradeChangelog(ulong studentId,
            DateTime? start = null,
            DateTime? end = null)
        {
            var response = await RawGetStudentGradeChangelog(studentId.ToString(), start, end);

            var redundant =
                JsonConvert.DeserializeObject<RedundantGradeChangeEventResponse>(
                    await response.Content.ReadAsStringAsync());

            return from model in redundant.Events
                select new GradeChangeEvent(this, model);
        }

        private Task<HttpResponseMessage> RawGetAssignmentGradeChangelog(string assignmentId,
            DateTime? start,
            DateTime? end)
        {
            var url = $"audit/grade_change/assignments/{assignmentId}";
            return _client.GetAsync(url + BuildQueryString(("start_time", start?.ToIso8601Date()),
                ("end_time", end?.ToIso8601Date())));
        }

        private Task<HttpResponseMessage> RawGetCourseGradeChangelog(string courseId,
            DateTime? start,
            DateTime? end)
        {
            var url = $"audit/grade_change/courses/{courseId}";
            return _client.GetAsync(url + BuildQueryString(("start_time", start?.ToIso8601Date()),
                ("end_time", end?.ToIso8601Date())));
        }

        private Task<HttpResponseMessage> RawGetGraderGradeChangelog(string graderId,
            DateTime? start,
            DateTime? end)
        {
            var url = $"audit/grade_change/graders/{graderId}";
            return _client.GetAsync(url + BuildQueryString(("start_time", start?.ToIso8601Date()),
                ("end_time", end?.ToIso8601Date())));
        }

        private Task<HttpResponseMessage> RawGetStudentGradeChangelog(string studentId,
            DateTime? start,
            DateTime? end)
        {
            var url = $"audit/grade_change/students/{studentId}";
            return _client.GetAsync(url + BuildQueryString(("start_time", start?.ToIso8601Date()),
                ("end_time", end?.ToIso8601Date())));
        }
    }
}