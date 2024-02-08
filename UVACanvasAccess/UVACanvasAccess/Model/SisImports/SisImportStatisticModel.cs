using Newtonsoft.Json;

namespace UVACanvasAccess.Model.SisImports
{
    internal class SisImportStatisticModel
    {
        [JsonProperty("concluded")] public ulong? Concluded { get; set; }

        [JsonProperty("created")] public ulong? Created { get; set; }

        [JsonProperty("deactivated")] public ulong? Deactivated { get; set; }

        [JsonProperty("deleted")] public ulong? Deleted { get; set; }

        [JsonProperty("restored")] public ulong? Restored { get; set; }
    }
}