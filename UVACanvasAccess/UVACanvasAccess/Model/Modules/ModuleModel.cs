using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Model.Modules
{
    internal class ModuleModel
    {
        [JsonProperty("id")] public ulong Id { get; set; }

        [JsonProperty("published")] public bool? Published { get; set; }

        [JsonProperty("publish_final_grade")] public bool? PublishFinalGrade { get; set; }

        [JsonProperty("require_sequential_progress")]
        public bool? RequireSequentialProgress { get; set; }

        [OptIn] [JsonProperty("completed_at")] public DateTime? CompletedAt { get; set; }

        [JsonProperty("unlock_at")] public DateTime? UnlockAt { get; set; }

        [OptIn]
        [CanBeNull]
        [Enigmatic] // can be null if "the module is deemed too large", even if opted-in
        [JsonProperty("items")]
        public IEnumerable<ModuleItemModel> Items { get; set; }

        [CanBeNull]
        [JsonProperty("prerequisite_module_ids")]
        public IEnumerable<ulong> PrerequisiteModuleIds { get; set; }

        [JsonProperty("items_url")] public string ItemsUrl { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [CanBeNull]
        [JsonProperty("state")]
        public string State { get; set; } // todo make sure this is an enum in Structure class

        [JsonProperty("workflow_state")] public string WorkflowState { get; set; }

        [JsonProperty("items_count")] public uint ItemsCount { get; set; }

        [JsonProperty("position")] public uint Position { get; set; }
    }
}