using JetBrains.Annotations;
using Newtonsoft.Json;

namespace UVACanvasAccess.Model.Enrollments
{
    internal class GradeModel
    {
        [JsonProperty("current_grade")]
        [CanBeNull]
        public string CurrentGrade { get; set; }

        [JsonProperty("current_score")]
        [CanBeNull]
        public string CurrentScore { get; set; }

        [JsonProperty("final_grade")]
        [CanBeNull]
        public string FinalGrade { get; set; }

        [JsonProperty("final_score")]
        [CanBeNull]
        public string FinalScore { get; set; }

        [JsonProperty("html_url")] [CanBeNull] public string HtmlUrl { get; set; }

        [JsonProperty("unposted_current_grade")]
        [CanBeNull]
        public string UnpostedCurrentGrade { get; set; }

        [JsonProperty("unposted_current_score")]
        [CanBeNull]
        public string UnpostedCurrentScore { get; set; }

        [JsonProperty("unposted_final_grade")]
        [CanBeNull]
        public string UnpostedFinalGrade { get; set; }

        [JsonProperty("unposted_final_score")]
        [CanBeNull]
        public string UnpostedFinalScore { get; set; }
    }
}