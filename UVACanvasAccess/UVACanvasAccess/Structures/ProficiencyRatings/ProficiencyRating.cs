using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.ProficiencyRatings;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Structures.ProficiencyRatings
{
    [PublicAPI]
    public class ProficiencyRating : IPrettyPrint
    {
        private readonly Api _api;

        internal ProficiencyRating(Api api, ProficiencyRatingModel model)
        {
            _api        = api;
            Description = model.Description;
            Points      = model.Points;
            Mastery     = model.Mastery;
            Color       = model.Color;
        }

        public bool Mastery { get; }

        public string Color { get; }

        public string Description { get; }

        public uint Points { get; }

        public string ToPrettyString() => "ProficiencyRating {" +
            ($"\n{nameof(Description)}: {Description}," +
                $"\n{nameof(Points)}: {Points}," +
                $"\n{nameof(Mastery)}: {Mastery}," +
                $"\n{nameof(Color)}: {Color}").Indent(4) +
            "\n}";
    }
}