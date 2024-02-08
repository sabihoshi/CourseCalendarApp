using Newtonsoft.Json;

namespace UVACanvasAccess.Model.Users
{
    internal class CourseNicknameModel
    {
        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("nickname")] public string Nickname { get; set; }

        [JsonProperty("course_id")] public ulong CourseId { get; set; }
    }
}