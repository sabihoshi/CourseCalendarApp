using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UVACanvasAccess.Model.Courses;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Structures.Courses
{
    [PublicAPI]
    public class CourseSettings : IPrettyPrint
    {
        internal CourseSettings(CourseSettingsModel model)
        {
            AllowFinalGradeOverride       = model.AllowFinalGradeOverride;
            AllowStudentDiscussionTopics  = model.AllowStudentDiscussionTopics;
            AllowStudentForumAttachments  = model.AllowStudentForumAttachments;
            AllowStudentDiscussionEditing = model.AllowStudentDiscussionEditing;
            GradingStandardEnabled        = model.GradingStandardEnabled;
            GradingStandardId             = model.GradingStandardId;
            AllowStudentOrganizedGroups   = model.AllowStudentOrganizedGroups;
            HideFinalGrades               = model.HideFinalGrades;
            HideDistributionGraphs        = model.HideDistributionGraphs;
            LockAllAnnouncements          = model.LockAllAnnouncements;
            RestrictStudentPastView       = model.RestrictStudentPastView;
            RestrictStudentFutureView     = model.RestrictStudentFutureView;
            ShowAnnouncementsOnHomePage   = model.ShowAnnouncementsOnHomePage;
            HomePageAnnouncementLimit     = model.HomePageAnnouncementLimit;
        }

        public CourseSettings(bool? allowFinalGradeOverride = null,
            bool? allowStudentDiscussionTopics = null,
            bool? allowStudentForumAttachments = null,
            bool? allowStudentDiscussionEditing = null,
            bool? gradingStandardEnabled = null,
            ulong? gradingStandardId = null,
            bool? allowStudentOrganizedGroups = null,
            bool? hideFinalGrades = null,
            bool? hideDistributionGraphs = null,
            bool? lockAllAnnouncements = null,
            bool? restrictStudentPastView = null,
            bool? restrictStudentFutureView = null,
            bool? showAnnouncementsOnHomePage = null,
            long? homePageAnnouncementLimit = null)
        {
            AllowFinalGradeOverride       = allowFinalGradeOverride;
            AllowStudentDiscussionTopics  = allowStudentDiscussionTopics;
            AllowStudentForumAttachments  = allowStudentForumAttachments;
            AllowStudentDiscussionEditing = allowStudentDiscussionEditing;
            GradingStandardEnabled        = gradingStandardEnabled;
            GradingStandardId             = gradingStandardId;
            AllowStudentOrganizedGroups   = allowStudentOrganizedGroups;
            HideFinalGrades               = hideFinalGrades;
            HideDistributionGraphs        = hideDistributionGraphs;
            LockAllAnnouncements          = lockAllAnnouncements;
            RestrictStudentPastView       = restrictStudentPastView;
            RestrictStudentFutureView     = restrictStudentFutureView;
            ShowAnnouncementsOnHomePage   = showAnnouncementsOnHomePage;
            HomePageAnnouncementLimit     = homePageAnnouncementLimit;
        }

        public bool? AllowFinalGradeOverride { get; }

        public bool? AllowStudentDiscussionEditing { get; }

        public bool? AllowStudentDiscussionTopics { get; }

        public bool? AllowStudentForumAttachments { get; }

        public bool? AllowStudentOrganizedGroups { get; }

        public bool? GradingStandardEnabled { get; }

        public bool? HideDistributionGraphs { get; }

        public bool? HideFinalGrades { get; }

        public bool? LockAllAnnouncements { get; }

        public bool? RestrictStudentFutureView { get; }

        public bool? RestrictStudentPastView { get; }

        public bool? ShowAnnouncementsOnHomePage { get; }

        public long? HomePageAnnouncementLimit { get; }

        public ulong? GradingStandardId { get; }

        public string ToPrettyString() => "CourseSettings {" +
            ($"\n{nameof(AllowFinalGradeOverride)}: {AllowFinalGradeOverride}," +
                $"\n{nameof(AllowStudentDiscussionTopics)}: {AllowStudentDiscussionTopics}," +
                $"\n{nameof(AllowStudentForumAttachments)}: {AllowStudentForumAttachments}," +
                $"\n{nameof(AllowStudentDiscussionEditing)}: {AllowStudentDiscussionEditing}," +
                $"\n{nameof(GradingStandardEnabled)}: {GradingStandardEnabled}," +
                $"\n{nameof(GradingStandardId)}: {GradingStandardId}," +
                $"\n{nameof(AllowStudentOrganizedGroups)}: {AllowStudentOrganizedGroups}," +
                $"\n{nameof(HideFinalGrades)}: {HideFinalGrades}," +
                $"\n{nameof(HideDistributionGraphs)}: {HideDistributionGraphs}," +
                $"\n{nameof(LockAllAnnouncements)}: {LockAllAnnouncements}," +
                $"\n{nameof(RestrictStudentPastView)}: {RestrictStudentPastView}," +
                $"\n{nameof(RestrictStudentFutureView)}: {RestrictStudentFutureView}," +
                $"\n{nameof(ShowAnnouncementsOnHomePage)}: {ShowAnnouncementsOnHomePage}," +
                $"\n{nameof(HomePageAnnouncementLimit)}: {HomePageAnnouncementLimit}").Indent(4) +
            "\n}";

        internal IEnumerable<(string, string)> GetTuples()
        {
            return new[]
            {
                ("allow_final_grade_override", AllowFinalGradeOverride?.ToShortString()),
                ("allow_student_discussion_topics", AllowStudentDiscussionTopics?.ToShortString()),
                ("allow_student_forum_attachments", AllowStudentForumAttachments?.ToShortString()),
                ("allow_student_discussion_editing", AllowStudentDiscussionEditing?.ToShortString()),
                ("grading_standard_enabled", GradingStandardEnabled?.ToShortString()),
                ("grading_standard_id", GradingStandardId?.ToString()),
                ("allow_student_organized_groups", AllowStudentOrganizedGroups?.ToShortString()),
                ("hide_final_grades", HideFinalGrades?.ToShortString()),
                ("hide_distribution_graphs", HideDistributionGraphs?.ToShortString()),
                ("lock_all_announcements", LockAllAnnouncements?.ToShortString()),
                ("restrict_student_past_view", RestrictStudentPastView?.ToShortString()),
                ("restrict_student_future_view", RestrictStudentFutureView?.ToShortString()),
                ("show_announcements_on_home_page", ShowAnnouncementsOnHomePage?.ToShortString()),
                ("home_page_announcement_limit", HomePageAnnouncementLimit?.ToString())
            }.Where(t => t.Item2 != null);
        }
    }
}