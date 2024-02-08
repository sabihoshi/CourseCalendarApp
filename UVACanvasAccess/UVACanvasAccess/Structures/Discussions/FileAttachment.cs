using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.Discussions;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Structures.Discussions
{
    [PublicAPI]
    public class FileAttachment : IPrettyPrint
    {
        private readonly Api _api;

        internal FileAttachment(Api api, FileAttachmentModel model)
        {
            _api        = api;
            ContentType = model.ContentType;
            Url         = model.Url;
            Filename    = model.Filename;
            DisplayName = model.DisplayName;
        }

        public string ContentType { get; }

        public string DisplayName { get; }

        public string Filename { get; }

        public string Url { get; }

        public string ToPrettyString() => "FileAttachment {" +
            ($"\n{nameof(ContentType)}: {ContentType}," +
                $"\n{nameof(Url)}: {Url}," +
                $"\n{nameof(Filename)}: {Filename}," +
                $"\n{nameof(DisplayName)}: {DisplayName}").Indent(4) +
            "\n}";
    }
}