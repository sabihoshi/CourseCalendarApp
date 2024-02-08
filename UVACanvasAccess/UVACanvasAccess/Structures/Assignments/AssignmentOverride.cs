using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.Assignments;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Structures.Assignments
{
    /// <summary>
    ///     Represents a change in dates for an <see cref="Assignment">assignment</see> for a subset of students.
    /// </summary>
    [PublicAPI]
    public class AssignmentOverride : IPrettyPrint
    {
        private readonly Api _api;

        internal AssignmentOverride(Api api, AssignmentOverrideModel model)
        {
            _api            = api;
            Id              = model.Id;
            AssignmentId    = model.AssignmentId;
            StudentIds      = model.StudentIds;
            GroupId         = model.GroupId;
            CourseSectionId = model.CourseSectionId;
            Title           = model.Title;
            DueAt           = model.DueAt;
            AllDay          = model.AllDay;
            AllDayDate      = model.AllDayDate;
            UnlockAt        = model.UnlockAt;
            LockAt          = model.LockAt;
        }

        /// <summary>
        ///     The override id.
        /// </summary>
        public ulong Id { get; }

        public bool? AllDay { get; }

        public DateTime? AllDayDate { get; }

        public DateTime? DueAt { get; }

        public DateTime? LockAt { get; }

        public DateTime? UnlockAt { get; }

        /// <summary>
        ///     The students this override applies to.
        /// </summary>
        [CanBeNull]
        public IEnumerable<ulong> StudentIds { get; }

        /// <summary>
        ///     The title of the override.
        /// </summary>
        public string Title { get; }

        /// <summary>
        ///     The assignment this override applies to.
        /// </summary>
        public ulong AssignmentId { get; }

        /// <summary>
        ///     The section this override applies to.
        /// </summary>
        public ulong CourseSectionId { get; }

        /// <summary>
        ///     The group this override applies to.
        /// </summary>
        public ulong? GroupId { get; }

        public string ToPrettyString() => "AssignmentOverride {" +
            ($"\n{nameof(Id)}: {Id}," +
                $"\n{nameof(AssignmentId)}: {AssignmentId}," +
                $"\n{nameof(StudentIds)}: {StudentIds?.ToPrettyString()}," +
                $"\n{nameof(GroupId)}: {GroupId}," +
                $"\n{nameof(CourseSectionId)}: {CourseSectionId}," +
                $"\n{nameof(Title)}: {Title}," +
                $"\n{nameof(DueAt)}: {DueAt}," +
                $"\n{nameof(AllDay)}: {AllDay}," +
                $"\n{nameof(AllDayDate)}: {AllDayDate}," +
                $"\n{nameof(UnlockAt)}: {UnlockAt}," +
                $"\n{nameof(LockAt)}: {LockAt}").Indent(4) +
            "\n}";

        internal AssignmentOverrideModel ToModel() => new AssignmentOverrideModel
        {
            Id              = Id,
            AssignmentId    = AssignmentId,
            StudentIds      = StudentIds,
            GroupId         = GroupId,
            CourseSectionId = CourseSectionId,
            Title           = Title,
            DueAt           = DueAt,
            AllDay          = AllDay,
            AllDayDate      = AllDayDate,
            UnlockAt        = UnlockAt,
            LockAt          = LockAt
        };
    }
}