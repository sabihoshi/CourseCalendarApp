using Newtonsoft.Json;

namespace UVACanvasAccess.Model.OutcomeResults
{
    internal class OutcomeRollupScoreModel
    {
        [JsonProperty("links")] public OutcomeRollupScoreLinksModel Links { get; set; }

        [JsonProperty("count")] public uint? Count { get; set; }

        [JsonProperty("score")] public uint? Score { get; set; }
    }
}