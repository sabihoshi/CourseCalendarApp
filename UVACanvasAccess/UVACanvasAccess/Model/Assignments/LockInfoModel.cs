using System;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace UVACanvasAccess.Model.Assignments
{
    internal class LockInfoModel
    {
        [JsonProperty("manually_locked")] public bool? ManuallyLocked { get; set; }

        [JsonProperty("lock_at")] public DateTime? LockAt { get; set; }

        [JsonProperty("unlock_at")] public DateTime? UnlockAt { get; set; }

        [CanBeNull]
        [JsonProperty("context_module")]
        public object ContextModule { get; set; }

        [JsonProperty("asset_string")] public string AssetString { get; set; }
    }
}