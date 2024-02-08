using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.ExternalTools;

namespace UVACanvasAccess.Structures.ExternalTools
{
    [PublicAPI]
    public class LinkSelectionLocation : ExternalToolLocation, IToolUrl, IToolMessageType, IToolText
    {
        internal LinkSelectionLocation(Api api, LinkSelectionModel model) : base(api, model.Enabled)
        {
            Url         = model.Url;
            MessageType = model.MessageType;
            Text        = model.Text;
        }

        public string MessageType { get; }

        public string Text { get; }

        public string Url { get; }
    }
}