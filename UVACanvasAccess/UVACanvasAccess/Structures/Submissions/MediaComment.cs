using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.Submissions;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Structures.Submissions
{
    [PublicAPI]
    public class MediaComment : IPrettyPrint
    {
        private readonly Api _api;

        internal MediaComment(Api api, MediaCommentModel model)
        {
            _api        = api;
            ContentType = model.ContentType;
            DisplayName = model.DisplayName;
            MediaId     = model.MediaId;
            MediaType   = model.MediaType;
            Url         = model.Url;
        }

        public string ContentType { get; }

        public string DisplayName { get; }

        public string MediaId { get; }

        public string MediaType { get; }

        public string Url { get; }

        public string ToPrettyString() => "MediaComment {" +
            ($"\n{nameof(ContentType)}: {ContentType}," +
                $"\n{nameof(DisplayName)}: {DisplayName}," +
                $"\n{nameof(MediaId)}: {MediaId}," +
                $"\n{nameof(MediaType)}: {MediaType}," +
                $"\n{nameof(Url)}: {Url}").Indent(4) +
            "\n}";
    }
}