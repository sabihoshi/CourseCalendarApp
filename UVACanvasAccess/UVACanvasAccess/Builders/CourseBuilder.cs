using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Structures.Courses;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Builders
{
    /// <summary>
    ///     Used to create courses using the builder pattern.
    ///     When all desired fields are set, call <see cref="Post" /> to execute the operation.
    /// </summary>
    [PublicAPI]
    public class CourseBuilder
    {
        [PublicAPI]
        public enum CourseEditAction : byte
        {
            [ApiRepresentation("claim")] Unpublish,
            [ApiRepresentation("offer")] Publish,
            [ApiRepresentation("conclude")] Conclude,
            [ApiRepresentation("delete")] Delete,
            [ApiRepresentation("undelete")] Undelete
        }

        private readonly Api _api;
        private readonly bool _isEditing;
        private readonly ulong? _id;

        internal CourseBuilder(Api api, bool isEditing, ulong? accountId, ulong? id = null)
        {
            _api       = api;
            _isEditing = isEditing;
            _id        = id;
            AccountId  = accountId;
        }

        internal Dictionary<string, string> Fields { get; } = new Dictionary<string, string>();

        internal ulong? AccountId { get; }

        /// <summary>
        ///     Make the course visible to all unauthenticated or authenticated users.
        /// </summary>
        /// <param name="public"></param>
        /// <returns>This builder.</returns>
        public CourseBuilder AsPublic(bool @public = true) => PutArr("is_public", @public.ToShortString());

        /// <summary>
        ///     Make the course visible to all authenticated users.
        /// </summary>
        /// <param name="publicToAuth"></param>
        /// <returns>This builder.</returns>
        public CourseBuilder AsPublicToAuthUsers(bool publicToAuth = true)
            => PutArr("is_public_to_auth_users", publicToAuth.ToShortString());

        /// <summary>
        ///     Enroll the current user immediately as a teacher in this course.
        /// </summary>
        /// <param name="enrollMe"></param>
        /// <returns>This builder.</returns>
        public CourseBuilder EnrollCurrentUser(bool enrollMe = true) => Put("enroll_me", enrollMe.ToShortString());

        /// <summary>
        ///     Make this course available to students immediately.
        /// </summary>
        /// <param name="offerNow"></param>
        /// <returns>This builder.</returns>
        public CourseBuilder OfferImmediately(bool offerNow = true) => Put("offer", offerNow.ToShortString());

        public CourseBuilder TakeAction(CourseEditAction action) => _isEditing
            ? PutArr("event", action.GetApiRepresentation())
            : this;

        /// <summary>
        ///     Try to recover a deleted course from SIS with a matching SIS id before creating this course.
        /// </summary>
        /// <param name="tryToRecover"></param>
        /// <returns>This builder.</returns>
        public CourseBuilder TryToRecoverFromSis(bool tryToRecover = true)
            => Put("enable_sis_reactivation", tryToRecover.ToShortString());

        /// <summary>
        ///     Apply assigment group weighting to final grades.
        /// </summary>
        /// <param name="weight"></param>
        /// <returns>This builder.</returns>
        public CourseBuilder WithAssignmentGroupWeight(bool weight = true)
            => PutArr("apply_assignment_group_weights", weight.ToShortString());

        /// <summary>
        ///     The course code of this course.
        /// </summary>
        /// <param name="courseCode"></param>
        /// <returns>This builder.</returns>
        public CourseBuilder WithCourseCode(string courseCode) => PutArr("course_code", courseCode);

        /// <summary>
        ///     The course format.
        /// </summary>
        /// <param name="format"></param>
        /// <returns>This builder.</returns>
        public CourseBuilder WithCourseFormat(CourseFormat format)
            => PutArr("course_format", format.GetApiRepresentation());

        /// <summary>
        ///     Only allow enrollments between this course's start and end dates.
        /// </summary>
        /// <param name="restrict"></param>
        /// <returns>This builder.</returns>
        public CourseBuilder WithDateRestrictedEnrollments(bool restrict = true)
            => PutArr("restrict_enrollments_to_course_dates", restrict.ToShortString());

        /// <summary>
        ///     The course's default view.
        /// </summary>
        /// <param name="defaultView"></param>
        /// <returns>This builder.</returns>
        public CourseBuilder WithDefaultView(CourseView defaultView)
            => PutArr("default_view", defaultView.GetApiRepresentation());

        /// <summary>
        ///     The end date of this course.
        /// </summary>
        /// <param name="end"></param>
        /// <returns>This builder.</returns>
        public CourseBuilder WithEndDate(DateTime end) => PutArr("end_at", end.ToIso8601Date());

        /// <summary>
        ///     The grading standard for this course.
        /// </summary>
        /// <param name="standard"></param>
        /// <returns>This builder.</returns>
        public CourseBuilder WithGradingStandard(ulong standard) => PutArr("grading_standard_id", standard.ToString());

        /// <summary>
        ///     Hide final grades in the student summary.
        /// </summary>
        /// <param name="hide"></param>
        /// <returns>This builder.</returns>
        public CourseBuilder WithHiddenFinalGrades(bool hide = true)
            => PutArr("hide_final_grades", hide.ToShortString());

        /// <summary>
        ///     The course's integration id.
        /// </summary>
        /// <param name="integration"></param>
        /// <returns>This builder.</returns>
        public CourseBuilder WithIntegrationId(string integration) => PutArr("integration_id", integration);

        /// <summary>
        ///     The license type for this course.
        /// </summary>
        /// <param name="license"></param>
        /// <returns>This builder.</returns>
        public CourseBuilder WithLicense(License license) => PutArr("license", license.GetApiRepresentation());

        /// <summary>
        ///     The name of this course.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>This builder.</returns>
        public CourseBuilder WithName(string name) => PutArr("name", name);

        /// <summary>
        ///     Allow open enrollment.
        /// </summary>
        /// <param name="openEnrollment"></param>
        /// <returns>This builder.</returns>
        public CourseBuilder WithOpenEnrollment(bool openEnrollment = true)
            => PutArr("open_enrollment", openEnrollment.ToShortString());

        /// <summary>
        ///     The course's public description.
        /// </summary>
        /// <param name="description"></param>
        /// <returns>This builder.</returns>
        public CourseBuilder WithPublicDescription(string description) => PutArr("public_description", description);

        /// <summary>
        ///     Make the syllabus public.
        /// </summary>
        /// <param name="publicSyllabus"></param>
        /// <returns>This builder.</returns>
        public CourseBuilder WithPublicSyllabus(bool publicSyllabus = true)
            => PutArr("public_syllabus", publicSyllabus.ToShortString());

        /// <summary>
        ///     Allow self enrollment.
        /// </summary>
        /// <param name="selfEnrollment"></param>
        /// <returns>This builder.</returns>
        public CourseBuilder WithSelfEnrollment(bool selfEnrollment = true)
            => PutArr("self_enrollment", selfEnrollment.ToShortString());

        /// <summary>
        ///     The course's SIS id.
        /// </summary>
        /// <param name="sis"></param>
        /// <returns>This builder.</returns>
        public CourseBuilder WithSisId(string sis) => PutArr("sis_course_id", sis);

        /// <summary>
        ///     The start date of this course.
        /// </summary>
        /// <param name="start"></param>
        /// <returns>This builder.</returns>
        public CourseBuilder WithStartDate(DateTime start) => PutArr("start_at", start.ToIso8601Date());

        /// <summary>
        ///     Allow students to attach files to forum posts.
        /// </summary>
        /// <param name="allowed"></param>
        /// <returns>This builder.</returns>
        public CourseBuilder WithStudentForumAttachments(bool allowed = true)
            => PutArr("allow_student_forum_attachments", allowed.ToShortString());

        /// <summary>
        ///     Allow students to edit the wiki.
        /// </summary>
        /// <param name="allowed"></param>
        /// <returns>This builder.</returns>
        public CourseBuilder WithStudentWikiEditing(bool allowed = true)
            => PutArr("allow_student_wiki_edits", allowed.ToShortString());

        /// <summary>
        ///     The course syllabus.
        /// </summary>
        /// <param name="body"></param>
        /// <returns>This builder.</returns>
        public CourseBuilder WithSyllabusBody(string body) => PutArr("syllabus_body", body);

        /// <summary>
        ///     The unique term id to create this course in.
        /// </summary>
        /// <param name="termId"></param>
        /// <returns>This builder.</returns>
        public CourseBuilder WithTermId(ulong termId) => PutArr("term_id", termId.ToString());

        /// <summary>
        ///     The time zone of this course.
        /// </summary>
        /// <param name="timeZone"></param>
        /// <returns>This builder.</returns>
        public CourseBuilder WithTimeZone(string timeZone) => PutArr("time_zone", timeZone);

        /// <summary>
        ///     Allow wiki comments.
        /// </summary>
        /// <param name="allowed"></param>
        /// <returns>This builder.</returns>
        public CourseBuilder WithWikiComments(bool allowed = true)
            => PutArr("allow_wiki_comments", allowed.ToShortString());

        /// <summary>
        ///     Creates the assignment using the fields in this builder.
        /// </summary>
        /// <returns>The newly created assignment.</returns>
        public Task<Course> Post()
        {
            if (!_isEditing) return _api.PostCreateCourse(this);

            Debug.Assert(_id != null, nameof(_id) + " != null");
            return _api.PutEditCourse((ulong) _id, this);
        }

        private CourseBuilder Put(string k, string v)
        {
            Fields[k] = v;
            return this;
        }

        private CourseBuilder PutArr(string k, string v)
        {
            Fields[$"course[{k}]"] = v;
            return this;
        }
    }
}