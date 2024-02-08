using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.Users;
using UVACanvasAccess.Structures.Enrollments;
using UVACanvasAccess.Util;
using static UVACanvasAccess.ApiParts.Api;
using static UVACanvasAccess.ApiParts.Api.CourseEnrollmentState;
using static UVACanvasAccess.ApiParts.Api.CourseEnrollmentType;

namespace UVACanvasAccess.Structures.Users
{
    [PublicAPI]
    public class User : IPrettyPrint, IAppointmentGroupParticipant
    {
        private readonly Api _api;

        private string _bio;

        private string _name;

        private string _shortName;

        private string _sortableName;

        private string _timeZone;

        internal User(Api api, UserModel model)
        {
            _api            = api;
            Id              = model.Id;
            _name           = model.Name;
            _sortableName   = model.SortableName;
            _shortName      = model.ShortName;
            SisUserId       = model.SisUserId;
            SisImportId     = model.SisImportId;
            IntegrationId   = model.IntegrationId;
            LoginId         = model.LoginId;
            AvatarUrl       = model.AvatarUrl;
            Enrollments     = model.Enrollments.SelectNotNull(m => new Enrollment(api, m));
            Email           = model.Email;
            Locale          = model.Locale;
            EffectiveLocale = model.EffectiveLocale;
            LastLogin       = model.LastLogin;
            _timeZone       = model.TimeZone;
            _bio            = model.Bio;
            Permissions     = model.Permissions ?? new Dictionary<string, bool>();
        }

        public ulong Id { get; }

        public DateTime? LastLogin { get; }

        public Dictionary<string, bool> Permissions { get; private set; }

        [Enigmatic] public IEnumerable<Enrollment> Enrollments { get; private set; }

        public string AvatarUrl { get; private set; }

        public string Bio
        {
            get => _bio;
            set
            {
                var _ = _api.EditUser(new[] { ("bio", value) }, Id).Result;
                _bio = value;
            }
        }

        public string EffectiveLocale { get; private set; }

        public string Email { get; private set; }

        public string IntegrationId { get; private set; }

        public string Locale { get; private set; }

        public string LoginId { get; private set; }

        public string Name
        {
            get => _name;
            set
            {
                var _ = _api.EditUser(new[] { ("name", value) }, Id).Result;
                _name = value;
            }
        }

        public string ShortName
        {
            get => _shortName;
            set
            {
                var _ = _api.EditUser(new[] { ("short_name", value) }, Id).Result;
                _shortName = value;
            }
        }

        public string SisUserId { get; private set; }

        public string SortableName
        {
            get => _sortableName;
            set
            {
                var _ = _api.EditUser(new[] { ("sortable_name", value) }, Id).Result;
                _sortableName = value;
            }
        }

        public string TimeZone
        {
            get => _timeZone;
            set
            {
                var _ = _api.EditUser(new[] { ("time_zone", value) }).Result;
                _timeZone = value;
            }
        }

        public ulong? SisImportId { get; private set; }

        public string ToPrettyString() => "User {" +
            ($"\n{nameof(Id)}: {Id}," +
                $"\n{nameof(Name)}: {Name}," +
                $"\n{nameof(SortableName)}: {SortableName}," +
                $"\n{nameof(ShortName)}: {ShortName}," +
                $"\n{nameof(SisUserId)}: {SisUserId}," +
                $"\n{nameof(SisImportId)}: {SisImportId}," +
                $"\n{nameof(IntegrationId)}: {IntegrationId}," +
                $"\n{nameof(LoginId)}: {LoginId}," +
                $"\n{nameof(AvatarUrl)}: {AvatarUrl}," +
                $"\n{nameof(Enrollments)}: {Enrollments.ToPrettyString()}," +
                $"\n{nameof(Email)}: {Email}," +
                $"\n{nameof(Locale)}: {Locale}," +
                $"\n{nameof(EffectiveLocale)}: {EffectiveLocale}," +
                $"\n{nameof(LastLogin)}: {LastLogin}," +
                $"\n{nameof(TimeZone)}: {TimeZone}," +
                $"\n{nameof(Bio)}: {Bio}," +
                $"\n{nameof(Permissions)}: {Permissions.ToPrettyString()}").Indent(4) +
            "\n}";

        /// <summary>
        ///     Streams all enrollments for this user.
        /// </summary>
        /// <param name="types">(Optional) The set of enrollment types to filter by.</param>
        /// <param name="states">(Optional) The set of enrollment states to filter by.</param>
        /// <param name="includes">(Optional) Data to include in the result.</param>
        /// <returns>The stream of enrollments.</returns>
        public IAsyncEnumerable<Enrollment> StreamEnrollments(IEnumerable<CourseEnrollmentType> types = null,
            IEnumerable<CourseEnrollmentState> states = null,
            CourseEnrollmentIncludes? includes = null)
            => _api.StreamUserEnrollments(Id, types, states, includes);

        public IAsyncEnumerable<PageView> StreamPageViews(DateTime? startDate = null, DateTime? endDate = null)
            => _api.StreamUserPageViews(Id, startDate, endDate);

        public Task<Profile> GetProfile() => _api.GetUserProfile(Id);

        /// <summary>
        ///     Returns whether or not this user is a teacher.
        ///     Specifically, whether or not this user has enrolled with the Teacher role in at least one course.
        /// </summary>
        /// <param name="currentCoursesOnly">
        ///     If true, this user is only considered a teacher if he is enrolled as a teacher in a currently active
        ///     course.
        /// </param>
        /// <returns>Whether or not this user is a teacher.</returns>
        public async ValueTask<bool> IsTeacher(bool currentCoursesOnly = false)
        {
            var state = currentCoursesOnly
                ? new[] { Active }
                : new CourseEnrollmentState[] { };
            return !await _api.StreamUserEnrollments(Id, new[] { TeacherEnrollment }, state).IsEmptyAsync();
        }
    }
}