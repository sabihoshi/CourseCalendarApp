using System.Collections.Generic;
using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.Assignments;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Structures.Assignments
{
    /// <summary>
    ///     Represents some rubric criteria.
    /// </summary>
    [PublicAPI]
    public class RubricCriteria : IPrettyPrint
    {
        private readonly Api _api;

        internal RubricCriteria(Api api, RubricCriteriaModel model)
        {
            _api              = api;
            Points            = model.Points;
            Id                = model.Id;
            LearningOutcomeId = model.LearningOutcomeId;
            VendorGuid        = model.VendorGuid;
            Description       = model.Description;
            LongDescription   = model.LongDescription;
            CriterionUseRange = model.CriterionUseRange;
            Ratings           = model.Ratings.SelectNotNull(m => new RubricRating(api, m));
            IgnoreForScoring  = model.IgnoreForScoring;
        }

        public string Id { get; }

        public bool? CriterionUseRange { get; }

        public bool? IgnoreForScoring { get; }

        [CanBeNull] public IEnumerable<RubricRating> Ratings { get; }

        public string Description { get; }

        [CanBeNull] public string LearningOutcomeId { get; }

        public string LongDescription { get; }

        [CanBeNull] public string VendorGuid { get; }

        public uint? Points { get; }

        public string ToPrettyString() => "RubricCriteria {" +
            ($"\n{nameof(Points)}: {Points}," +
                $"\n{nameof(Id)}: {Id}," +
                $"\n{nameof(LearningOutcomeId)}: {LearningOutcomeId}," +
                $"\n{nameof(VendorGuid)}: {VendorGuid}," +
                $"\n{nameof(Description)}: {Description}," +
                $"\n{nameof(LongDescription)}: {LongDescription}," +
                $"\n{nameof(CriterionUseRange)}: {CriterionUseRange}," +
                $"\n{nameof(Ratings)}: {Ratings?.ToPrettyString()}," +
                $"\n{nameof(IgnoreForScoring)}: {IgnoreForScoring}").Indent(4) +
            "\n}";
    }
}