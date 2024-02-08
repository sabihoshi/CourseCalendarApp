using Newtonsoft.Json;

namespace UVACanvasAccess.Model.ExternalTools
{
    internal class EditorButtonModel
    {
        [JsonProperty("enabled")] public bool? Enabled { get; set; }

        [JsonProperty("icon_url")] public string IconUrl { get; set; }

        [JsonProperty("message_type")] public string MessageType { get; set; }

        [JsonProperty("url")] public string Url { get; set; }

        [JsonProperty("selection_height")] public uint? SelectionHeight { get; set; }

        [JsonProperty("selection_width")] public uint? SelectionWidth { get; set; }
    }
}