using Newtonsoft.Json;

namespace UVACanvasAccess.Model.ExternalTools
{
    internal class ToolConfigurationModel
    {
        [JsonProperty("enabled")] public bool? Enabled { get; set; }

        [JsonProperty("prefer_sis_email")] public bool? PreferSisEmail { get; set; }

        [JsonProperty("message_type")] public string MessageType { get; set; }

        [JsonProperty("url")] public string Url { get; set; }
    }
}