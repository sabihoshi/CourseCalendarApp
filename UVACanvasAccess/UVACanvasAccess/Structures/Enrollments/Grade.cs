using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.Enrollments;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Structures.Enrollments
{
    [PublicAPI]
    public class Grade : IPrettyPrint
    {
        private readonly Api _api;

        internal Grade(Api api, GradeModel model)
        {
            _api                 = api;
            HtmlUrl              = model.HtmlUrl;
            CurrentGrade         = model.CurrentGrade;
            FinalGrade           = model.FinalGrade;
            CurrentScore         = model.CurrentScore;
            FinalScore           = model.FinalScore;
            UnpostedCurrentGrade = model.UnpostedCurrentGrade;
            UnpostedFinalGrade   = model.UnpostedFinalGrade;
            UnpostedCurrentScore = model.UnpostedCurrentScore;
            UnpostedFinalScore   = model.UnpostedFinalScore;
        }

        [CanBeNull] public string CurrentGrade { get; }

        [CanBeNull] public string CurrentScore { get; }

        [CanBeNull] public string FinalGrade { get; }

        [CanBeNull] public string FinalScore { get; }

        [CanBeNull] public string HtmlUrl { get; }

        [CanBeNull] public string UnpostedCurrentGrade { get; }

        [CanBeNull] public string UnpostedCurrentScore { get; }

        [CanBeNull] public string UnpostedFinalGrade { get; }

        [CanBeNull] public string UnpostedFinalScore { get; }

        public string ToPrettyString() => "Grade {" +
            ($"\n{nameof(HtmlUrl)}: {HtmlUrl}," +
                $"\n{nameof(CurrentGrade)}: {CurrentGrade}," +
                $"\n{nameof(FinalGrade)}: {FinalGrade}," +
                $"\n{nameof(CurrentScore)}: {CurrentScore}," +
                $"\n{nameof(FinalScore)}: {FinalScore}," +
                $"\n{nameof(UnpostedCurrentGrade)}: {UnpostedCurrentGrade}," +
                $"\n{nameof(UnpostedFinalGrade)}: {UnpostedFinalGrade}," +
                $"\n{nameof(UnpostedCurrentScore)}: {UnpostedCurrentScore}," +
                $"\n{nameof(UnpostedFinalScore)}: {UnpostedFinalScore}").Indent(4) +
            "\n}";
    }
}