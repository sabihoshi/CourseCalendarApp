using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.Assignments;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Structures.Assignments
{
    [PublicAPI]
    public class TurnitinSettings : IPrettyPrint
    {
        private readonly Api _api;

        internal TurnitinSettings(Api api, TurnitinSettingsModel model)
        {
            _api                        = api;
            OriginalityReportVisibility = model.OriginalityReportVisibility;
            SPaperCheck                 = model.SPaperCheck;
            InternetCheck               = model.InternetCheck;
            JournalCheck                = model.JournalCheck;
            ExcludeBiblio               = model.ExcludeBiblio;
            ExcludeQuoted               = model.ExcludeQuoted;
            ExcludeSmallMatchesType     = model.ExcludeSmallMatchesType;
            ExcludeSmallMatchesValue    = model.ExcludeSmallMatchesValue;
        }

        public string ExcludeBiblio { get; }

        public string ExcludeQuoted { get; }

        public string ExcludeSmallMatchesType { get; }

        public string InternetCheck { get; }

        public string JournalCheck { get; }

        public string OriginalityReportVisibility { get; }

        public string SPaperCheck { get; }

        public uint ExcludeSmallMatchesValue { get; }

        public string ToPrettyString() => "TurnitinSettings {" +
            ($"\n{nameof(OriginalityReportVisibility)}: {OriginalityReportVisibility}," +
                $"\n{nameof(SPaperCheck)}: {SPaperCheck}," +
                $"\n{nameof(InternetCheck)}: {InternetCheck}," +
                $"\n{nameof(JournalCheck)}: {JournalCheck}," +
                $"\n{nameof(ExcludeBiblio)}: {ExcludeBiblio}," +
                $"\n{nameof(ExcludeQuoted)}: {ExcludeQuoted}," +
                $"\n{nameof(ExcludeSmallMatchesType)}: {ExcludeSmallMatchesType}," +
                $"\n{nameof(ExcludeSmallMatchesValue)}: {ExcludeSmallMatchesValue}").Indent(4) +
            "\n}";

        internal TurnitinSettingsModel ToModel() => new TurnitinSettingsModel
        {
            OriginalityReportVisibility = OriginalityReportVisibility,
            SPaperCheck                 = SPaperCheck,
            InternetCheck               = InternetCheck,
            JournalCheck                = JournalCheck,
            ExcludeBiblio               = ExcludeBiblio,
            ExcludeQuoted               = ExcludeQuoted,
            ExcludeSmallMatchesType     = ExcludeSmallMatchesType,
            ExcludeSmallMatchesValue    = ExcludeSmallMatchesValue
        };
    }
}