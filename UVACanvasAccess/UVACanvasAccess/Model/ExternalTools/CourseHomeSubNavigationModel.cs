using Newtonsoft.Json;

namespace UVACanvasAccess.Model.ExternalTools
{
    internal class CourseHomeSubNavigationModel
    {
        [JsonProperty("enabled")] public bool? Enabled { get; set; }

        [JsonProperty("icon_url")] public string IconUrl { get; set; }

        [JsonProperty("text")] public string Text { get; set; }

        [JsonProperty("url")] public string Url { get; set; }
    }
}