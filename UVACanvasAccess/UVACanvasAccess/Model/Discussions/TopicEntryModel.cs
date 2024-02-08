using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace UVACanvasAccess.Model.Discussions
{
    internal class TopicEntryModel
    {
        [JsonProperty("id")] public ulong Id { get; set; }

        [JsonProperty("forced_read_state")] public bool ForcedReadState { get; set; }

        [JsonProperty("has_more_replies")] public bool? HasMoreReplies { get; set; }

        [JsonProperty("created_at")] public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")] public DateTime? UpdatedAt { get; set; }

        [JsonProperty("attachment")]
        [CanBeNull]
        public FileAttachmentModel Attachment { get; set; }

        [JsonProperty("recent_replies")]
        [CanBeNull]
        public IEnumerable<TopicReplyModel> RecentReplies { get; set; }

        [JsonProperty("message")] public string Message { get; set; }

        [JsonProperty("read_state")] public string ReadState { get; set; }

        [JsonProperty("user_name")] public string UserName { get; set; }

        [JsonProperty("user_id")] public ulong UserId { get; set; }

        [JsonProperty("editor_id")] public ulong? EditorId { get; set; }
    }
}