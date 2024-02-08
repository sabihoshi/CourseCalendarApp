using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.Groups;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Structures.Groups
{
    [PublicAPI]
    public class GroupMembership : IPrettyPrint
    {
        private readonly Api _api;

        internal GroupMembership(Api api, GroupMembershipModel model)
        {
            _api          = api;
            Id            = model.Id;
            GroupId       = model.GroupId;
            UserId        = model.UserId;
            WorkflowState = model.WorkflowState;
            Moderator     = model.Moderator;
            JustCreated   = model.JustCreated;
            SisImportId   = model.SisImportId;
        }

        public ulong Id { get; }

        public bool Moderator { get; }

        public bool? JustCreated { get; }

        public string WorkflowState { get; }

        public ulong GroupId { get; }

        public ulong UserId { get; }

        public ulong? SisImportId { get; }

        public string ToPrettyString() => "GroupMembership {" +
            ($"\n{nameof(Id)}: {Id}," +
                $"\n{nameof(GroupId)}: {GroupId}," +
                $"\n{nameof(UserId)}: {UserId}," +
                $"\n{nameof(WorkflowState)}: {WorkflowState}," +
                $"\n{nameof(Moderator)}: {Moderator}," +
                $"\n{nameof(JustCreated)}: {JustCreated}," +
                $"\n{nameof(SisImportId)}: {SisImportId}").Indent(4) +
            "\n}";
    }
}