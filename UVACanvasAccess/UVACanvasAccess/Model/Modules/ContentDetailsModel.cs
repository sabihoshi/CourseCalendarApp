using System;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace UVACanvasAccess.Model.Modules
{
    internal class ContentDetailsModel
    {
        [JsonProperty("locked_for_user")] public bool? LockedForUser { get; set; }

        [JsonProperty("due_at")] public DateTime? DueAt { get; set; }

        [JsonProperty("lock_at")] public DateTime? LockAt { get; set; }

        [JsonProperty("unlock_at")] public DateTime? UnlockAt { get; set; }

        [CanBeNull]
        [JsonProperty("lock_explanation")]
        public string LockExplanation { get; set; }

        [JsonProperty("points_possible")] public uint? PointsPossible { get; set; }
    }
}