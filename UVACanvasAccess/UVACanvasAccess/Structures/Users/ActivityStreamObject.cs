using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.Users;
using UVACanvasAccess.Structures.Assignments;
using UVACanvasAccess.Structures.Courses;
using UVACanvasAccess.Structures.Submissions;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Structures.Users
{
    [PublicAPI]
    public abstract class ActivityStreamObject
    {
        protected readonly Api Api;

        internal ActivityStreamObject(Api api, ActivityStreamObjectModel model)
        {
            Api         = api;
            CreatedAt   = model.CreatedAt;
            UpdatedAt   = model.UpdatedAt;
            Id          = model.Id;
            Title       = model.Title;
            BodyMessage = model.Message;
            Type        = model.Type;
            ReadState   = model.ReadState;
            ContextType = model.ContextType;
            CourseId    = model.CourseId;
            GroupId     = model.GroupId;
            HtmlUrl     = model.HtmlUrl;
        }

        public ulong Id { get; }

        public bool ReadState { get; }

        public DateTime CreatedAt { get; }

        public DateTime? UpdatedAt { get; }

        public string BodyMessage { get; }

        public string ContextType { get; }

        public string HtmlUrl { get; }

        public string Title { get; }

        public string Type { get; }

        public ulong? CourseId { get; }

        public ulong? GroupId { get; }

        internal static ActivityStreamObject FromModel(Api api, ActivityStreamObjectModel model)
        {
            return model.Type switch
            {
                "DiscussionTopic" => new DiscussionTopic(api, model),
                "Announcement" => new Announcement(api, model),
                "Conversation" => new Conversation(api, model),
                "Message" => new Message(api, model),
                "Conference" => new Conference(api, model),
                "Collaboration" => new Collaboration(api, model),
                "AssignmentRequest" => new AssignmentRequest(api, model),
                "Submission" => new SubmissionObject(api, model),
                _ => throw new NotImplementedException("unknown ActivityStreamObject type " + model.Type)
            };
        }

        public class SubmissionObject : ActivityStreamObject
        {
            internal SubmissionObject(Api api, ActivityStreamObjectModel model) : base(api, model)
            {
                //Debug.Assert(model.AssignmentId != null, "model.AssignmentId != null");
                //Debug.Assert(model.UserId != null, "model.UserId != null");

                AssignmentId                  = model.AssignmentId;
                Assignment                    = model.Assignment.ConvertIfNotNull(m => new Assignment(api, m));
                Course                        = model.Course.ConvertIfNotNull(c => new Course(api, c));
                Attempt                       = model.Attempt;
                Body                          = model.Body;
                Grade                         = model.Grade;
                GradeMatchesCurrentSubmission = model.GradeMatchesCurrentSubmission;
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

            [CanBeNull] public Course Course { get; }

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

            public string LatePolicyStatus { get; }

            public string PreviewUrl { get; }

            public string SubmissionType { get; }

            [CanBeNull] public string Url { get; }

            public string WorkflowState { get; }

            public uint? Attempt { get; }

            public uint? ExtraAttempts { get; }

            public ulong? AssignmentId { get; }

            public ulong? UserId { get; }

            public User User { get; }
        }

        public class DiscussionTopic : ActivityStreamObject
        {
            internal DiscussionTopic(Api api, ActivityStreamObjectModel model) : base(api, model)
            {
                Debug.Assert(model.DiscussionTopicId != null, "model.DiscussionTopicId != null");

                TotalRootDiscussionEntries = model.TotalRootDiscussionEntries;
                RequireInitialPost         = model.RequireInitialPost;
                UserHasPosted              = model.UserHasPosted;
                RootDiscussionEntries      = model.RootDiscussionEntries;
                DiscussionTopicId          = (ulong) model.DiscussionTopicId;
            }

            public bool? RequireInitialPost { get; }

            public bool? UserHasPosted { get; }

            public object RootDiscussionEntries { get; } // todo this class/model

            public uint? TotalRootDiscussionEntries { get; }

            public ulong DiscussionTopicId { get; }
        }

        public class Announcement : ActivityStreamObject
        {
            internal Announcement(Api api, ActivityStreamObjectModel model) : base(api, model)
            {
                Debug.Assert(model.AnnouncementId != null, "model.AnnouncementId != null");

                TotalRootDiscussionEntries = model.TotalRootDiscussionEntries;
                RequireInitialPost         = model.RequireInitialPost;
                UserHasPosted              = model.UserHasPosted;
                RootDiscussionEntries      = model.RootDiscussionEntries;
                AnnouncementId             = (ulong) model.AnnouncementId;
            }

            public bool? RequireInitialPost { get; }

            public bool? UserHasPosted { get; }

            public object RootDiscussionEntries { get; } // todo this class/model

            public uint? TotalRootDiscussionEntries { get; }

            public ulong AnnouncementId { get; }
        }

        public class Conversation : ActivityStreamObject
        {
            internal Conversation(Api api, ActivityStreamObjectModel model) : base(api, model)
            {
                Debug.Assert(model.ConversationId != null, "model.ConversationId != null");

                ConversationId   = (ulong) model.ConversationId;
                Private          = model.Private;
                ParticipantCount = model.ParticipantCount;
            }

            public bool? Private { get; }

            public uint? ParticipantCount { get; }

            public ulong ConversationId { get; }
        }

        public class Message : ActivityStreamObject
        {
            internal Message(Api api, ActivityStreamObjectModel model) : base(api, model)
            {
                MessageId            = model.MessageId;
                NotificationCategory = model.NotificationCategory;
            }

            public string NotificationCategory { get; }

            public ulong? MessageId { get; }
        }

        public class Conference : ActivityStreamObject
        {
            internal Conference(Api api, ActivityStreamObjectModel model) : base(api, model)
            {
                Debug.Assert(model.WebConferenceId != null, "model.WebConferenceId != null");

                WebConferenceId = (ulong) model.WebConferenceId;
            }

            public ulong WebConferenceId { get; }
        }

        public class Collaboration : ActivityStreamObject
        {
            internal Collaboration(Api api, ActivityStreamObjectModel model) : base(api, model)
            {
                Debug.Assert(model.CollaborationId != null, "model.CollaborationId != null");

                CollaborationId = (ulong) model.CollaborationId;
            }

            public ulong CollaborationId { get; }
        }

        public class AssignmentRequest : ActivityStreamObject
        {
            internal AssignmentRequest(Api api, ActivityStreamObjectModel model) : base(api, model)
            {
                Debug.Assert(model.AssignmentRequestId != null, "model.AssignmentRequestId != null");

                AssignmentRequestId = (ulong) model.AssignmentRequestId;
            }

            public ulong AssignmentRequestId { get; }
        }
    }
}