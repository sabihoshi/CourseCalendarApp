using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UVACanvasAccess.Model.Modules
{
    internal class ModuleItemSequenceNodeModel
    {
        [CanBeNull]
        [JsonProperty("mastery_path")]
        public JObject MasteryPath { get; set; } // todo concrete type?

        [JsonProperty("current")] public ModuleItemModel Current { get; set; }

        [CanBeNull] [JsonProperty("next")] public ModuleItemModel Next { get; set; }

        [CanBeNull] [JsonProperty("prev")] public ModuleItemModel Prev { get; set; }
    }
}