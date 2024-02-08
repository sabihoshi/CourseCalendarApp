using Newtonsoft.Json;

namespace UVACanvasAccess.Model.Analytics
{
    internal class TardinessModel
    {
        [JsonProperty("floating")] public decimal Floating { get; set; }

        [JsonProperty("late")] public decimal Late { get; set; }

        [JsonProperty("missing")] public decimal Missing { get; set; }

        [JsonProperty("on_time")] public decimal OnTime { get; set; }

        [JsonProperty("total")] public decimal Total { get; set; }
    }
}