using Newtonsoft.Json;

namespace UVACanvasAccess.Model.Analytics
{
    internal class DepartmentStatisticsModel
    {
        [JsonProperty("assignments")] public ulong Assignments { get; set; }

        [JsonProperty("attachments")] public ulong Attachments { get; set; }

        [JsonProperty("courses")] public ulong Courses { get; set; }

        [JsonProperty("discussion_topics")] public ulong DiscussionTopics { get; set; }

        [JsonProperty("media_objects")] public ulong MediaObjects { get; set; }

        [JsonProperty("students")] public ulong Students { get; set; }

        [JsonProperty("subaccounts")] public ulong Subaccounts { get; set; }

        [JsonProperty("teacher")] public ulong Teachers { get; set; }
    }
}