using Newtonsoft.Json;

namespace UVACanvasAccess.Model.Users
{
    internal class AvatarModel
    {
        [JsonProperty("id")] public ulong Id { get; set; }

        [JsonProperty("content_type")] public string ContentType { get; set; }

        [JsonProperty("display_name")] public string DisplayName { get; set; }

        [JsonProperty("filename")] public string Filename { get; set; }

        [JsonProperty("token")] public string Token { get; set; }

        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("url")] public string Url { get; set; }

        [JsonProperty("size")] public ulong Size { get; set; }
    }
}