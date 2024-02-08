using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UVACanvasAccess.Model.Assignments;
using UVACanvasAccess.Model.Courses;
using UVACanvasAccess.Model.Users;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Model.Submissions
{
    internal class SubmissionModel
    {
        [CanBeNull]
        [JsonProperty("assignment")]
        public AssignmentModel Assignment { get; set; }

        [JsonProperty("assignment_visible")] public bool? AssignmentVisible { get; set; }

        [JsonProperty("excused")] public bool? Excused { get; set; }

        [JsonProperty("grade_matches_current_submission")]
        public bool? GradeMatchesCurrentSubmission { get; set; }

        [JsonProperty("late")] public bool? Late { get; set; }

        [JsonProperty("missing")] public bool? Missing { get; set; }

        [CanBeNull] [JsonProperty("course")] public CourseModel Course { get; set; }

        [JsonProperty("graded_at")] public DateTime? GradedAt { get; set; }

        [JsonProperty("submitted_at")] public DateTime? SubmittedAt { get; set; }

        [JsonProperty("score")] public decimal? Score { get; set; }

        [JsonProperty("points_deducted")] public double? PointsDeducted { get; set; }

        [JsonProperty("seconds_late")] public double? SecondsLate { get; set; }

        [CanBeNull]
        [JsonProperty("submission_comments")]
        public IEnumerable<SubmissionCommentModel> SubmissionComments { get; set; }

        [JsonProperty("grader_id")]
        [Enigmatic]
        public long? GraderId { get; set; } // why can this be negative???

        [CanBeNull]
        [JsonProperty("anonymous_id")]
        public string AnonymousId { get; set; }

        [CanBeNull] [JsonProperty("body")] public string Body { get; set; }

        [JsonProperty("grade")] public string Grade { get; set; }

        [JsonProperty("html_url")] public string HtmlUrl { get; set; }

        [JsonProperty("late_policy_status")] public string LatePolicyStatus { get; set; }

        [JsonProperty("preview_url")] public string PreviewUrl { get; set; }

        [JsonProperty("submission_type")] public string SubmissionType { get; set; }

        [CanBeNull] [JsonProperty("url")] public string Url { get; set; }

        [JsonProperty("workflow_state")] public string WorkflowState { get; set; }

        [JsonProperty("attempt")] public uint? Attempt { get; set; }

        [JsonProperty("extra_attempts")] public uint? ExtraAttempts { get; set; }

        [JsonProperty("assignment_id")] public ulong AssignmentId { get; set; }

        [JsonProperty("user_id")] public ulong UserId { get; set; }

        [JsonProperty("user")] public UserModel User { get; set; }
    }
}