using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace UVACanvasAccess.Model.Assignments
{
    internal class RubricCriteriaModel
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("criterion_use_range")] public bool? CriterionUseRange { get; set; }

        [JsonProperty("ignore_for_scoring")] public bool? IgnoreForScoring { get; set; }

        [CanBeNull] [JsonProperty("ratings")] public IEnumerable<RubricRatingModel> Ratings { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [CanBeNull]
        [JsonProperty("learning_outcome_id")]
        public string LearningOutcomeId { get; set; }

        [JsonProperty("long_description")] public string LongDescription { get; set; }

        [CanBeNull]
        [JsonProperty("vendor_guid")]
        public string VendorGuid { get; set; }

        [JsonProperty("points")] public uint? Points { get; set; }
    }
}