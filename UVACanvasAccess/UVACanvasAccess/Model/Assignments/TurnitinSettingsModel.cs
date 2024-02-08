using Newtonsoft.Json;

namespace UVACanvasAccess.Model.Assignments
{
    internal class TurnitinSettingsModel
    {
        [JsonProperty("exclude_biblio")] public string ExcludeBiblio { get; set; }

        [JsonProperty("exclude_quoted")] public string ExcludeQuoted { get; set; }

        [JsonProperty("exclude_small_matches_type")]
        public string ExcludeSmallMatchesType { get; set; }

        [JsonProperty("internet_check")] public string InternetCheck { get; set; }

        [JsonProperty("journal_check")] public string JournalCheck { get; set; }

        [JsonProperty("originality_report_visibility")]
        public string OriginalityReportVisibility { get; set; }

        [JsonProperty("s_paper_check")] public string SPaperCheck { get; set; }

        [JsonProperty("exclude_small_matches_value")]
        public uint ExcludeSmallMatchesValue { get; set; }
    }
}