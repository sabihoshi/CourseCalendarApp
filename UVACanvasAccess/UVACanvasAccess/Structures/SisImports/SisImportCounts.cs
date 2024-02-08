using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.SisImports;

namespace UVACanvasAccess.Structures.SisImports
{
    [PublicAPI]
    public class SisImportCounts
    {
        private readonly Api _api;

        internal SisImportCounts(Api api, SisImportCountsModel model)
        {
            _api                    = api;
            Accounts                = model.Accounts;
            Terms                   = model.Terms;
            AbstractCourses         = model.AbstractCourses;
            Courses                 = model.Courses;
            Sections                = model.Sections;
            CrossLists              = model.CrossLists;
            Users                   = model.Users;
            Enrollments             = model.Enrollments;
            Groups                  = model.Groups;
            GroupMemberships        = model.GroupMemberships;
            GradePublishingResults  = model.GradePublishingResults;
            BatchCoursesDeleted     = model.BatchCoursesDeleted ?? 0;
            BatchSectionsDeleted    = model.BatchSectionsDeleted ?? 0;
            BatchEnrollmentsDeleted = model.BatchEnrollmentsDeleted ?? 0;
            Errors                  = model.Errors;
            Warnings                = model.Warnings;
        }

        public ulong AbstractCourses { get; }

        public ulong Accounts { get; }

        public ulong BatchCoursesDeleted { get; }

        public ulong BatchEnrollmentsDeleted { get; }

        public ulong BatchSectionsDeleted { get; }

        public ulong Courses { get; }

        public ulong CrossLists { get; }

        public ulong Enrollments { get; }

        public ulong Errors { get; }

        public ulong GradePublishingResults { get; }

        public ulong GroupMemberships { get; }

        public ulong Groups { get; }

        public ulong Sections { get; }

        public ulong Terms { get; }

        public ulong Users { get; }

        public ulong Warnings { get; }

        public override string ToString()
            => $"{nameof(Accounts)}: {Accounts}, {nameof(Terms)}: {Terms}, {nameof(AbstractCourses)}: {AbstractCourses}, {nameof(Courses)}: {Courses}, {nameof(Sections)}: {Sections}, {nameof(CrossLists)}: {CrossLists}, {nameof(Users)}: {Users}, {nameof(Enrollments)}: {Enrollments}, {nameof(Groups)}: {Groups}, {nameof(GroupMemberships)}: {GroupMemberships}, {nameof(GradePublishingResults)}: {GradePublishingResults}, {nameof(BatchCoursesDeleted)}: {BatchCoursesDeleted}, {nameof(BatchSectionsDeleted)}: {BatchSectionsDeleted}, {nameof(BatchEnrollmentsDeleted)}: {BatchEnrollmentsDeleted}, {nameof(Errors)}: {Errors}, {nameof(Warnings)}: {Warnings}";
    }
}