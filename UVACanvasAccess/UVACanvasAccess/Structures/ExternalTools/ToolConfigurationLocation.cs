using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.ExternalTools;

namespace UVACanvasAccess.Structures.ExternalTools
{
    [PublicAPI]
    public class ToolConfigurationLocation : ExternalToolLocation, IToolUrl, IToolMessageType
    {
        internal ToolConfigurationLocation(Api api, ToolConfigurationModel model) : base(api, model.Enabled)
        {
            Url            = model.Url;
            MessageType    = model.MessageType;
            PreferSisEmail = model.PreferSisEmail;
        }

        public bool? PreferSisEmail { get; }

        public string MessageType { get; }

        public string Url { get; }
    }
}