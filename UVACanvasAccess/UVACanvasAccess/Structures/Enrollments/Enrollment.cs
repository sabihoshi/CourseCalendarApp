using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.Enrollments;
using UVACanvasAccess.Structures.Users;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Structures.Enrollments
{
    [PublicAPI]
    public class Enrollment : IPrettyPrint
    {
        private readonly Api _api;

        internal Enrollment(Api api, EnrollmentModel model)
        {
            _api                              = api;
            Id                                = model.Id;
            CourseId                          = model.CourseId;
            SisCourseId                       = model.SisCourseId;
            CourseIntegrationId               = model.CourseIntegrationId;
            CourseSectionId                   = model.CourseSectionId;
            SectionIntegrationId              = model.SectionIntegrationId;
            SisAccountId                      = model.SisAccountId;
            SisSectionId                      = model.SisSectionId;
            SisUserId                         = model.SisUserId;
            EnrollmentState                   = model.EnrollmentState;
            LimitPrivilegesToCourseSection    = model.LimitPrivilegesToCourseSection;
            SisImportId                       = model.SisImportId;
            RootAccountId                     = model.RootAccountId;
            Type                              = model.Type;
            UserId                            = model.UserId;
            AssociatedUserId                  = model.AssociatedUserId;
            Role                              = model.Role;
            RoleId                            = model.RoleId;
            CreatedAt                         = model.CreatedAt;
            UpdatedAt                         = model.UpdatedAt;
            StartAt                           = model.StartAt;
            EndAt                             = model.EndAt;
            LastActivityAt                    = model.LastActivityAt;
            LastAttendedAt                    = model.LastAttendedAt;
            TotalActivityTime                 = model.TotalActivityTime;
            HtmlUrl                           = model.HtmlUrl;
            Grades                            = model.Grades.ConvertIfNotNull(m => new Grade(api, m));
            User                              = model.User.ConvertIfNotNull(m => new UserDisplay(api, m));
            OverrideGrade                     = model.OverrideGrade;
            OverrideScore                     = model.OverrideScore;
            UnpostedCurrentGrade              = model.UnpostedCurrentGrade;
            UnpostedFinalGrade                = model.UnpostedFinalGrade;
            UnpostedCurrentScore              = model.UnpostedCurrentScore;
            UnpostedFinalScore                = model.UnpostedFinalScore;
            HasGradingPeriods                 = model.HasGradingPeriods;
            TotalsForAllGradingPeriodsOption  = model.TotalsForAllGradingPeriodsOption;
            CurrentGradingPeriodTitle         = model.CurrentGradingPeriodTitle;
            CurrentGradingPeriodId            = model.CurrentGradingPeriodId;
            CurrentPeriodOverrideGrade        = model.CurrentPeriodOverrideGrade;
            CurrentPeriodOverrideScore        = model.CurrentPeriodOverrideScore;
            CurrentPeriodUnpostedFinalScore   = model.CurrentPeriodUnpostedFinalScore;
            CurrentPeriodUnpostedCurrentGrade = model.CurrentPeriodUnpostedCurrentGrade;
            CurrentPeriodUnpostedFinalGrade   = model.CurrentPeriodUnpostedFinalGrade;
        }

        public ulong Id { get; }

        [CanBeNull] public bool? HasGradingPeriods { get; }

        public bool? LimitPrivilegesToCourseSection { get; }

        [CanBeNull] public bool? TotalsForAllGradingPeriodsOption { get; }

        public DateTime? CreatedAt { get; }

        public DateTime? EndAt { get; }

        public DateTime? LastActivityAt { get; }

        public DateTime? LastAttendedAt { get; }

        public DateTime? StartAt { get; }

        public DateTime? UpdatedAt { get; }

        [CanBeNull] public decimal? CurrentPeriodOverrideScore { get; }

        [CanBeNull] public decimal? CurrentPeriodUnpostedFinalScore { get; }

        public decimal? OverrideScore { get; }

        public Grade Grades { get; }

        [CanBeNull] public string CourseIntegrationId { get; }

        [CanBeNull] public string CurrentGradingPeriodTitle { get; }

        [CanBeNull] public string CurrentPeriodOverrideGrade { get; }

        [CanBeNull] public string CurrentPeriodUnpostedCurrentGrade { get; }

        [CanBeNull] public string CurrentPeriodUnpostedFinalGrade { get; }

        public string EnrollmentState { get; }

        public string HtmlUrl { get; }

        public string OverrideGrade { get; }

        public string Role { get; }

        [CanBeNull] public string SectionIntegrationId { get; }

        [CanBeNull] public string SisAccountId { get; }

        [CanBeNull] public string SisCourseId { get; }

        [CanBeNull] public string SisSectionId { get; }

        [CanBeNull] public string SisUserId { get; }

        public string Type { get; }

        [CanBeNull] public string UnpostedCurrentGrade { get; }

        [CanBeNull] public string UnpostedCurrentScore { get; }

        [CanBeNull] public string UnpostedFinalGrade { get; }

        [CanBeNull] public string UnpostedFinalScore { get; }

        public ulong CourseId { get; }

        public ulong RoleId { get; }

        public ulong UserId { get; }

        public ulong? AssociatedUserId { get; }

        public ulong? CourseSectionId { get; }

        [CanBeNull] public ulong? CurrentGradingPeriodId { get; }

        public ulong? RootAccountId { get; }

        public ulong? SisImportId { get; }

        public ulong? TotalActivityTime { get; }

        public UserDisplay User { get; }

        public string ToPrettyString() => "Enrollment {" +
            ($"\n{nameof(Id)}: {Id}," +
                $"\n{nameof(CourseId)}: {CourseId}," +
                $"\n{nameof(SisCourseId)}: {SisCourseId}," +
                $"\n{nameof(CourseIntegrationId)}: {CourseIntegrationId}," +
                $"\n{nameof(CourseSectionId)}: {CourseSectionId}," +
                $"\n{nameof(SectionIntegrationId)}: {SectionIntegrationId}," +
                $"\n{nameof(SisAccountId)}: {SisAccountId}," +
                $"\n{nameof(SisSectionId)}: {SisSectionId}," +
                $"\n{nameof(SisUserId)}: {SisUserId}," +
                $"\n{nameof(EnrollmentState)}: {EnrollmentState}," +
                $"\n{nameof(LimitPrivilegesToCourseSection)}: {LimitPrivilegesToCourseSection}," +
                $"\n{nameof(SisImportId)}: {SisImportId}," +
                $"\n{nameof(RootAccountId)}: {RootAccountId}," +
                $"\n{nameof(Type)}: {Type}," +
                $"\n{nameof(UserId)}: {UserId}," +
                $"\n{nameof(AssociatedUserId)}: {AssociatedUserId}," +
                $"\n{nameof(Role)}: {Role}," +
                $"\n{nameof(RoleId)}: {RoleId}," +
                $"\n{nameof(CreatedAt)}: {CreatedAt}," +
                $"\n{nameof(UpdatedAt)}: {UpdatedAt}," +
                $"\n{nameof(StartAt)}: {StartAt}," +
                $"\n{nameof(EndAt)}: {EndAt}," +
                $"\n{nameof(LastActivityAt)}: {LastActivityAt}," +
                $"\n{nameof(LastAttendedAt)}: {LastAttendedAt}," +
                $"\n{nameof(TotalActivityTime)}: {TotalActivityTime}," +
                $"\n{nameof(HtmlUrl)}: {HtmlUrl}," +
                $"\n{nameof(Grades)}: {Grades?.ToPrettyString()}," +
                $"\n{nameof(User)}: {User?.ToPrettyString()}," +
                $"\n{nameof(OverrideGrade)}: {OverrideGrade}," +
                $"\n{nameof(OverrideScore)}: {OverrideScore}," +
                $"\n{nameof(UnpostedCurrentGrade)}: {UnpostedCurrentGrade}," +
                $"\n{nameof(UnpostedFinalGrade)}: {UnpostedFinalGrade}," +
                $"\n{nameof(UnpostedCurrentScore)}: {UnpostedCurrentScore}," +
                $"\n{nameof(UnpostedFinalScore)}: {UnpostedFinalScore}," +
                $"\n{nameof(HasGradingPeriods)}: {HasGradingPeriods}," +
                $"\n{nameof(TotalsForAllGradingPeriodsOption)}: {TotalsForAllGradingPeriodsOption}," +
                $"\n{nameof(CurrentGradingPeriodTitle)}: {CurrentGradingPeriodTitle}," +
                $"\n{nameof(CurrentGradingPeriodId)}: {CurrentGradingPeriodId}," +
                $"\n{nameof(CurrentPeriodOverrideGrade)}: {CurrentPeriodOverrideGrade}," +
                $"\n{nameof(CurrentPeriodOverrideScore)}: {CurrentPeriodOverrideScore}," +
                $"\n{nameof(CurrentPeriodUnpostedFinalScore)}: {CurrentPeriodUnpostedFinalScore}," +
                $"\n{nameof(CurrentPeriodUnpostedCurrentGrade)}: {CurrentPeriodUnpostedCurrentGrade}," +
                $"\n{nameof(CurrentPeriodUnpostedFinalGrade)}: {CurrentPeriodUnpostedFinalGrade}").Indent(4) +
            "\n}";

        /// <summary>
        ///     Concludes this enrollment without deleting it. <br />
        ///     This is the same action that occurs automatically when the user reaches the end of their time in the course,
        ///     such as at the end of the school year.
        /// </summary>
        /// <returns>The concluded enrollment.</returns>
        /// <remarks>
        ///     This object will be outdated once the operation completes. Use the returned object instead.
        /// </remarks>
        [Pure]
        public Task<Enrollment> Conclude() => _api.ConcludeEnrollment(CourseId, Id);

        /// <summary>
        ///     Sets an enrollment to <see cref="Api.CourseEnrollmentState.Inactive" />.
        /// </summary>
        /// <returns>The inactivated enrollment.</returns>
        /// <remarks>
        ///     This object will be outdated once the operation completes. Use the returned object instead.
        /// </remarks>
        [Pure]
        public Task<Enrollment> Deactivate() => _api.DeactivateEnrollment(CourseId, Id);

        /// <summary>
        ///     Irrecoverably deletes this enrollment.
        /// </summary>
        /// <returns>The deleted enrollment.</returns>
        /// <remarks>
        ///     This object will be outdated once the operation completes. Use the returned object instead.
        /// </remarks>
        [Pure]
        public Task<Enrollment> Delete() => _api.DeleteEnrollment(CourseId, Id);
    }
}