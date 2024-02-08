using Newtonsoft.Json;

namespace UVACanvasAccess.Model.ExternalTools
{
    internal class MigrationSelectionModel
    {
        [JsonProperty("enabled")] public bool? Enabled { get; set; }

        [JsonProperty("message_type")] public string MessageType { get; set; }

        [JsonProperty("url")] public string Url { get; set; }
    }
}