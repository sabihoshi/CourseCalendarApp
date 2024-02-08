using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.ExternalTools;

namespace UVACanvasAccess.Structures.ExternalTools
{
    [PublicAPI]
    public class HomeworkSubmissionLocation : ExternalToolLocation, IToolUrl, IToolMessageType, IToolText
    {
        internal HomeworkSubmissionLocation(Api api, HomeworkSubmissionModel model) : base(api, model.Enabled)
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