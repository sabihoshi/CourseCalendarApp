using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.Assignments;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Structures.Assignments
{
    /// <summary>
    ///     Represents one rating as part of a rubric.
    /// </summary>
    [PublicAPI]
    public class RubricRating : IPrettyPrint
    {
        private readonly Api _api;

        internal RubricRating(Api api, RubricRatingModel model)
        {
            _api            = api;
            Points          = model.Points;
            Id              = model.Id;
            Description     = model.Description;
            LongDescription = model.LongDescription;
        }

        public string Id { get; }

        public string Description { get; }

        public string LongDescription { get; }

        public uint Points { get; }

        public string ToPrettyString() => "RubricRating {" +
            ($"\n{nameof(Points)}: {Points}," +
                $"\n{nameof(Id)}: {Id}," +
                $"\n{nameof(Description)}: {Description}," +
                $"\n{nameof(LongDescription)}: {LongDescription}").Indent(4) +
            "\n}";
    }
}