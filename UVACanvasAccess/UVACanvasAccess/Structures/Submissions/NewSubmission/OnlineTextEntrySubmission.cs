using System.Collections.Generic;
using UVACanvasAccess.Util;

// ReSharper disable MemberCanBePrivate.Global
namespace UVACanvasAccess.Structures.Submissions.NewSubmission
{
    /// <summary>
    ///     Represents the submission of text, as if it was entered into the online interface.
    /// </summary>
    public class OnlineTextEntrySubmission : INewSubmissionContent
    {
        public OnlineTextEntrySubmission(string body)
        {
            Type = ApiSubmissionType.OnlineTextEntry;
            Body = body;
        }

        public string Body { get; }

        public IEnumerable<(string, string)> GetTuples() => ("submission[body]", Body).Yield();

        public ApiSubmissionType Type { get; }
    }
}