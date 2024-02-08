using System;
using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.Courses;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Structures.Courses
{
    [PublicAPI]
    public class CourseProgress : IPrettyPrint
    {
        private readonly Api _api;

        internal CourseProgress(Api api, CourseProgressModel model)
        {
            _api                      = api;
            RequirementCount          = model.RequirementCount;
            RequirementCompletedCount = model.RequirementCompletedCount;
            NextRequirementUrl        = model.NextRequirementUrl;
            CompletedAt               = model.CompletedAt;
        }

        public DateTime? CompletedAt { get; }

        [CanBeNull] public string NextRequirementUrl { get; }

        public uint? RequirementCompletedCount { get; }

        public uint? RequirementCount { get; }

        public string ToPrettyString() => "CourseProgress {" +
            ($"\n{nameof(RequirementCount)}: {RequirementCount}," +
                $"\n{nameof(RequirementCompletedCount)}: {RequirementCompletedCount}," +
                $"\n{nameof(NextRequirementUrl)}: {NextRequirementUrl}," +
                $"\n{nameof(CompletedAt)}: {CompletedAt}").Indent(4) +
            "\n}";
    }
}