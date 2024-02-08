using System.Collections.Generic;
using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.Groups;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Structures.Groups
{
    [PublicAPI]
    public class Group : IPrettyPrint, IAppointmentGroupParticipant
    {
        private readonly Api _api;

        internal Group(Api api, GroupModel model)
        {
            _api            = api;
            Id              = model.Id;
            Name            = model.Name;
            Description     = model.Description;
            IsPublic        = model.IsPublic;
            FollowedByUser  = model.FollowedByUser;
            JoinLevel       = model.JoinLevel;
            MembersCount    = model.MembersCount;
            AvatarUrl       = model.AvatarUrl;
            ContextType     = model.ContextType;
            CourseId        = model.CourseId;
            AccountId       = model.AccountId;
            Role            = model.Role;
            GroupCategoryId = model.GroupCategoryId;
            SisGroupId      = model.SisGroupId;
            SisImportId     = model.SisImportId;
            StorageQuoteMb  = model.StorageQuoteMb;
            Permissions     = model.Permissions;
        }

        public ulong Id { get; }

        public bool FollowedByUser { get; }

        public bool? IsPublic { get; }

        public Dictionary<string, bool> Permissions { get; }

        public string AvatarUrl { get; }

        public string ContextType { get; }

        [CanBeNull] public string Description { get; }

        public string JoinLevel { get; }

        public string Name { get; }

        public string Role { get; }

        [CanBeNull] public string SisGroupId { get; }

        public uint MembersCount { get; }

        public uint StorageQuoteMb { get; }

        public ulong GroupCategoryId { get; }

        public ulong? AccountId { get; }

        public ulong? CourseId { get; }

        public ulong? SisImportId { get; }

        public string ToPrettyString() => "Group {" +
            ($"\n{nameof(Id)}: {Id}," +
                $"\n{nameof(Name)}: {Name}," +
                $"\n{nameof(Description)}: {Description}," +
                $"\n{nameof(IsPublic)}: {IsPublic}," +
                $"\n{nameof(FollowedByUser)}: {FollowedByUser}," +
                $"\n{nameof(JoinLevel)}: {JoinLevel}," +
                $"\n{nameof(MembersCount)}: {MembersCount}," +
                $"\n{nameof(AvatarUrl)}: {AvatarUrl}," +
                $"\n{nameof(ContextType)}: {ContextType}," +
                $"\n{nameof(CourseId)}: {CourseId}," +
                $"\n{nameof(AccountId)}: {AccountId}," +
                $"\n{nameof(Role)}: {Role}," +
                $"\n{nameof(GroupCategoryId)}: {GroupCategoryId}," +
                $"\n{nameof(SisGroupId)}: {SisGroupId}," +
                $"\n{nameof(SisImportId)}: {SisImportId}," +
                $"\n{nameof(StorageQuoteMb)}: {StorageQuoteMb}," +
                $"\n{nameof(Permissions)}: {Permissions.ToPrettyString()}").Indent(4) +
            "\n}";
    }
}