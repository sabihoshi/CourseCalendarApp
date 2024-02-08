using System.Collections.Generic;
using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.Reports;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Structures.Reports
{
    [PublicAPI]
    public class ReportDescription : IPrettyPrint
    {
        private readonly Api _api;

        internal ReportDescription(Api api, ReportDescriptionModel model)
        {
            _api   = api;
            Report = model.Report;
            Title  = model.Title;
            Parameters = model.Parameters?.ValSelect(m => new ReportParameterDescription(m))
                ?? new Dictionary<string, ReportParameterDescription>();
        }

        [NotNull] public Dictionary<string, ReportParameterDescription> Parameters { get; }

        public string Report { get; }

        public string Title { get; }

        public string ToPrettyString() => "ReportDescription {" +
            ($"\n{nameof(Report)}: {Report}," +
                $"\n{nameof(Title)}: {Title}," +
                $"\n{nameof(Parameters)}: {Parameters.ToPrettyString()}").Indent(4) +
            "\n}";
    }

    public class ReportParameterDescription : IPrettyPrint
    {
        internal ReportParameterDescription(ReportParameterDescriptionModel model)
        {
            Description = model.Description;
            Required    = model.Required;
        }

        public bool Required { get; }

        public string Description { get; }

        public string ToPrettyString() => "ReportParameterDescription {" +
            ($"\n{nameof(Description)}: {Description}," +
                $"\n{nameof(Required)}: {Required}").Indent(4) +
            "\n}";
    }
}