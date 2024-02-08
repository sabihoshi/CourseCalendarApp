using System;
using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.GradeChangelog;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Structures.GradeChangelog
{
    [PublicAPI]
    public class GradeChangeEvent : IPrettyPrint
    {
        private readonly Api _api;

        internal GradeChangeEvent(Api api, GradeChangeEventModel model)
        {
            _api              = api;
            Id                = model.Id;
            CreatedAt         = model.CreatedAt;
            EventType         = model.EventType;
            ExcusedAfter      = model.ExcusedAfter;
            ExcusedBefore     = model.ExcusedBefore;
            GradeAfter        = model.GradeAfter;
            GradeBefore       = model.GradeBefore;
            GradedAnonymously = model.GradedAnonymously ?? false;
            VersionNumber     = model.VersionNumber;
            RequestId         = model.RequestId;
            Links             = model.Links.ConvertIfNotNull(m => new GradeChangeEventLinks(api, m));
        }

        public string Id { get; }

        public bool ExcusedAfter { get; }

        public bool ExcusedBefore { get; }

        public bool GradedAnonymously { get; }

        public DateTime CreatedAt { get; }

        [CanBeNull] public GradeChangeEventLinks Links { get; }

        public string EventType { get; }

        public string GradeAfter { get; }

        public string GradeBefore { get; }

        public string RequestId { get; }

        public string VersionNumber { get; }

        public string ToPrettyString() => "GradeChangeEvent {" +
            ($"\n{nameof(Id)}: {Id}," +
                $"\n{nameof(CreatedAt)}: {CreatedAt}," +
                $"\n{nameof(EventType)}: {EventType}," +
                $"\n{nameof(ExcusedAfter)}: {ExcusedAfter}," +
                $"\n{nameof(ExcusedBefore)}: {ExcusedBefore}," +
                $"\n{nameof(GradeAfter)}: {GradeAfter}," +
                $"\n{nameof(GradeBefore)}: {GradeBefore}," +
                $"\n{nameof(GradedAnonymously)}: {GradedAnonymously}," +
                $"\n{nameof(VersionNumber)}: {VersionNumber}," +
                $"\n{nameof(RequestId)}: {RequestId}," +
                $"\n{nameof(Links)}: {Links?.ToPrettyString()}").Indent(4) +
            "\n}";
    }
}