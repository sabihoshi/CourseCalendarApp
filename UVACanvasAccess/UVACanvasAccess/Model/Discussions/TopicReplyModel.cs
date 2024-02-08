using System;
using Newtonsoft.Json;

namespace UVACanvasAccess.Model.Discussions
{
    internal class TopicReplyModel
    {
        [JsonProperty("id")] public ulong Id { get; set; }

        [JsonProperty("forced_read_state")] public bool? ForcedReadState { get; set; }

        [JsonProperty("created_at")] public DateTime CreatedAt { get; set; }

        [JsonProperty("message")] public string Message { get; set; }

        [JsonProperty("read_state")] public string ReadState { get; set; }

        [JsonProperty("user_name")] public string UserName { get; set; }

        [JsonProperty("user_id")] public ulong UserId { get; set; }

        [JsonProperty("editor_id")] public ulong? EditorId { get; set; }
    }
}