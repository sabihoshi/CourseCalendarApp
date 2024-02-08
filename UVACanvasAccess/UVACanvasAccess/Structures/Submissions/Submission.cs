using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.Submissions;
using UVACanvasAccess.Structures.Assignments;
using UVACanvasAccess.Structures.Courses;
using UVACanvasAccess.Structures.Users;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Structures.Submissions
{
    [PublicAPI]
    public class Submission : IPrettyPrint
    {
        protected readonly Api Api;

        internal Submission(Api api, SubmissionModel model)
        {
            Api                           = api;
            AssignmentId                  = model.AssignmentId;
            Assignment                    = model.Assignment.ConvertIfNotNull(m => new Assignment(api, m));
            Course                        = model.Course.ConvertIfNotNull(c => new Course(api, c));
            Attempt                       = model.Attempt;
            Body                          = model.Body;
            Grade                         = model.Grade;
            GradeMatchesCurrentSubmission = model.GradeMatchesCurrentSubmission;
            HtmlUrl                       = model.HtmlUrl;
            PreviewUrl                    = model.PreviewUrl;
            Score                         = model.Score;
            SubmissionComments
                = model.SubmissionComments.ConvertIfNotNull(ie => ie.Select(m => new SubmissionComment(api, m)));
            SubmissionType    = model.SubmissionType;
            SubmittedAt       = model.SubmittedAt;
            Url               = model.Url;
            UserId            = model.UserId;
            GraderId          = model.GraderId;
            GradedAt          = model.GradedAt;
            User              = model.User.ConvertIfNotNull(m => new User(api, m));
            Late              = model.Late;
            AssignmentVisible = model.AssignmentVisible;
            Excused           = model.Excused;
            Missing           = model.Missing;
            LatePolicyStatus  = model.LatePolicyStatus;
            PointsDeducted    = model.PointsDeducted;
            SecondsLate       = model.SecondsLate;
            WorkflowState     = model.WorkflowState;
            ExtraAttempts     = model.ExtraAttempts;
            AnonymousId       = model.AnonymousId;
        }

        [CanBeNull] public Assignment Assignment { get; }

        public bool? AssignmentVisible { get; }

        public bool? Excused { get; }

        public bool? GradeMatchesCurrentSubmission { get; }

        public bool? Late { get; }

        public bool? Missing { get; }

        [CanBeNull] public Course Course { get; set; } // bug remove setter

        public DateTime? GradedAt { get; }

        public DateTime? SubmittedAt { get; }

        public decimal? Score { get; }

        public double? PointsDeducted { get; }

        public double? SecondsLate { get; }

        [CanBeNull] public IEnumerable<SubmissionComment> SubmissionComments { get; }

        [Enigmatic] public long? GraderId { get; }

        [CanBeNull] public string AnonymousId { get; }

        [CanBeNull] public string Body { get; }

        public string Grade { get; }

        public string HtmlUrl { get; }

        public string LatePolicyStatus { get; }

        public string PreviewUrl { get; }

        public string SubmissionType { get; }

        [CanBeNull] public string Url { get; }

        public string WorkflowState { get; }

        public uint? Attempt { get; }

        public uint? ExtraAttempts { get; }

        public ulong AssignmentId { get; }

        public ulong UserId { get; }

        public User User { get; }

        public string ToPrettyString() => "Submission {" +
            ($"\n{nameof(AssignmentId)}: {AssignmentId}," +
                $"\n{nameof(Assignment)}: {Assignment}," +
                $"\n{nameof(Course)}: {Course}," +
                $"\n{nameof(Attempt)}: {Attempt}," +
                $"\n{nameof(Body)}: {Body}," +
                $"\n{nameof(Grade)}: {Grade}," +
                $"\n{nameof(GradeMatchesCurrentSubmission)}: {GradeMatchesCurrentSubmission}," +
                $"\n{nameof(HtmlUrl)}: {HtmlUrl}," +
                $"\n{nameof(PreviewUrl)}: {PreviewUrl}," +
                $"\n{nameof(Score)}: {Score}," +
                $"\n{nameof(SubmissionComments)}: {SubmissionComments?.ToPrettyString()}," +
                $"\n{nameof(SubmissionType)}: {SubmissionType}," +
                $"\n{nameof(SubmittedAt)}: {SubmittedAt}," +
                $"\n{nameof(Url)}: {Url}," +
                $"\n{nameof(UserId)}: {UserId}," +
                $"\n{nameof(GraderId)}: {GraderId}," +
                $"\n{nameof(GradedAt)}: {GradedAt}," +
                $"\n{nameof(User)}: {User}," +
                $"\n{nameof(Late)}: {Late}," +
                $"\n{nameof(AssignmentVisible)}: {AssignmentVisible}," +
                $"\n{nameof(Excused)}: {Excused}," +
                $"\n{nameof(Missing)}: {Missing}," +
                $"\n{nameof(LatePolicyStatus)}: {LatePolicyStatus}," +
                $"\n{nameof(PointsDeducted)}: {PointsDeducted}," +
                $"\n{nameof(SecondsLate)}: {SecondsLate}," +
                $"\n{nameof(WorkflowState)}: {WorkflowState}," +
                $"\n{nameof(ExtraAttempts)}: {ExtraAttempts}," +
                $"\n{nameof(AnonymousId)}: {AnonymousId}").Indent(4) +
            "\n}";
    }
}