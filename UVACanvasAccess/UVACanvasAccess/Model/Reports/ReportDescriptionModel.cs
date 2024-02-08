using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace UVACanvasAccess.Model.Reports
{
    // ReSharper disable MemberCanBePrivate.Global
    // ReSharper disable ClassNeverInstantiated.Global
    internal class ReportDescriptionModel
    {
        [CanBeNull]
        [JsonProperty("parameters")]
        public Dictionary<string, ReportParameterDescriptionModel> Parameters { get; set; }

        [JsonProperty("report")] public string Report { get; set; }

        [JsonProperty("title")] public string Title { get; set; }
    }

    internal class ReportParameterDescriptionModel
    {
        [JsonProperty("required")] public bool Required { get; set; }

        [JsonProperty("description")] public string Description { get; set; }
    }
}