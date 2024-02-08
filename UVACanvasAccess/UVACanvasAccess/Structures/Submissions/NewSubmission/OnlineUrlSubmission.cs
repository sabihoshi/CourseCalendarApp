using System.Collections.Generic;
using UVACanvasAccess.Util;

// ReSharper disable MemberCanBePrivate.Global
namespace UVACanvasAccess.Structures.Submissions.NewSubmission
{
    /// <summary>
    ///     Represents the submission of a URL.
    /// </summary>
    public class OnlineUrlSubmission : INewSubmissionContent
    {
        public OnlineUrlSubmission(string url)
        {
            Type = ApiSubmissionType.OnlineUrl;
            Url  = url;
        }

        public string Url { get; }

        public IEnumerable<(string, string)> GetTuples() => ("submission[body]", Url).Yield();

        public ApiSubmissionType Type { get; }
    }
}