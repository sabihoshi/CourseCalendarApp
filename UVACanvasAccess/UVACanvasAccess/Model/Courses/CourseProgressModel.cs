using System;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace UVACanvasAccess.Model.Courses
{
    internal class CourseProgressModel
    {
        [JsonProperty("completed_at")] public DateTime? CompletedAt { get; set; }

        [JsonProperty("next_requirement_url")]
        [CanBeNull]
        public string NextRequirementUrl { get; set; }

        [JsonProperty("requirement_completed_count")]
        public uint? RequirementCompletedCount { get; set; }

        [JsonProperty("requirement_count")] public uint? RequirementCount { get; set; }
    }
}