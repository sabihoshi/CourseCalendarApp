using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.Conversations;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Structures.Conversations
{
    [PublicAPI]
    public class ConversationParticipant : IPrettyPrint
    {
        private readonly Api _api;

        internal ConversationParticipant(Api api, ConversationParticipantModel model)
        {
            _api      = api;
            Id        = model.Id;
            Name      = model.Name;
            FullName  = model.FullName;
            AvatarUrl = model.AvatarUrl;
        }

        public ulong Id { get; }

        [CanBeNull] public string AvatarUrl { get; }

        public string FullName { get; }

        public string Name { get; }

        public string ToPrettyString() => "ConversationParticipant {" +
            ($"\n{nameof(Id)}: {Id}," +
                $"\n{nameof(Name)}: {Name}," +
                $"\n{nameof(FullName)}: {FullName}," +
                $"\n{nameof(AvatarUrl)}: {AvatarUrl}").Indent(4) +
            "\n}";
    }
}