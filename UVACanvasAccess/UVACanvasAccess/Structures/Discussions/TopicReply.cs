using System;
using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.Discussions;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Structures.Discussions
{
    [PublicAPI]
    public class TopicReply : IPrettyPrint
    {
        private readonly Api _api;

        internal TopicReply(Api api, TopicReplyModel model)
        {
            _api            = api;
            Id              = model.Id;
            UserId          = model.UserId;
            EditorId        = model.EditorId;
            UserName        = model.UserName;
            Message         = model.Message;
            ReadState       = model.ReadState;
            ForcedReadState = model.ForcedReadState;
            CreatedAt       = model.CreatedAt;
        }

        public ulong Id { get; }

        public bool? ForcedReadState { get; }

        public DateTime CreatedAt { get; }

        public string Message { get; }

        public string ReadState { get; }

        public string UserName { get; }

        public ulong UserId { get; }

        public ulong? EditorId { get; }

        public string ToPrettyString() => "TopicReply {" +
            ($"\n{nameof(Id)}: {Id}," +
                $"\n{nameof(UserId)}: {UserId}," +
                $"\n{nameof(EditorId)}: {EditorId}," +
                $"\n{nameof(UserName)}: {UserName}," +
                $"\n{nameof(Message)}: {Message}," +
                $"\n{nameof(ReadState)}: {ReadState}," +
                $"\n{nameof(ForcedReadState)}: {ForcedReadState}," +
                $"\n{nameof(CreatedAt)}: {CreatedAt}").Indent(4) +
            "\n}";
    }
}