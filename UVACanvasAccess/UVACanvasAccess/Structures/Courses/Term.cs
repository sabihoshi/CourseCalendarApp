using System;
using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.Courses;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Structures.Courses
{
    [PublicAPI]
    public class Term : IPrettyPrint
    {
        private readonly Api _api;

        internal Term(Api api, TermModel model)
        {
            _api    = api;
            Id      = model.Id;
            Name    = model.Name;
            StartAt = model.StartAt;
            EndAt   = model.EndAt;
        }

        public ulong Id { get; }

        public DateTime? EndAt { get; }

        public DateTime? StartAt { get; }

        public string Name { get; }

        public string ToPrettyString() => "Term {" +
            ($"\n{nameof(Id)}: {Id}," +
                $"\n{nameof(Name)}: {Name}," +
                $"\n{nameof(StartAt)}: {StartAt}," +
                $"\n{nameof(EndAt)}: {EndAt}").Indent(4) +
            "\n}";
    }
}