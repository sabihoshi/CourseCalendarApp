using JetBrains.Annotations;
using UVACanvasAccess.Model.Analytics;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Structures.Analytics
{
    [PublicAPI]
    public class Tardiness : IPrettyPrint
    {
        internal Tardiness(TardinessModel model)
        {
            Missing  = model.Missing;
            Late     = model.Late;
            OnTime   = model.OnTime;
            Floating = model.Floating;
            Total    = model.Total;
        }

        public decimal Floating { get; }

        public decimal Late { get; }

        public decimal Missing { get; }

        public decimal OnTime { get; }

        public decimal Total { get; }

        public string ToPrettyString() => "Tardiness {" +
            ($"\n{nameof(Missing)}: {Missing}," +
                $"\n{nameof(Late)}: {Late}," +
                $"\n{nameof(OnTime)}: {OnTime}," +
                $"\n{nameof(Floating)}: {Floating}," +
                $"\n{nameof(Total)}: {Total}").Indent(4) +
            "\n}";
    }
}