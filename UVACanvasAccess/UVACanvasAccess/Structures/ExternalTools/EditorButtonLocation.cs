using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.ExternalTools;

namespace UVACanvasAccess.Structures.ExternalTools
{
    [PublicAPI]
    public class EditorButtonLocation : ExternalToolLocation, IToolUrl, IToolSelectionDimensions, IToolIconUrl,
        IToolMessageType
    {
        internal EditorButtonLocation(Api api, EditorButtonModel model) : base(api, model.Enabled)
        {
            Url             = model.Url;
            SelectionHeight = model.SelectionHeight;
            SelectionWidth  = model.SelectionWidth;
            IconUrl         = model.IconUrl;
            MessageType     = model.MessageType;
        }

        public string IconUrl { get; }

        public string MessageType { get; }

        public uint? SelectionHeight { get; }

        public uint? SelectionWidth { get; }

        public string Url { get; }
    }
}