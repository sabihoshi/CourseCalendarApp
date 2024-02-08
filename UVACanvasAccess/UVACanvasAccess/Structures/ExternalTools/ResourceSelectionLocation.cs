using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.ExternalTools;

namespace UVACanvasAccess.Structures.ExternalTools
{
    [PublicAPI]
    public class ResourceSelectionLocation : ExternalToolLocation, IToolUrl, IToolIconUrl, IToolSelectionDimensions
    {
        internal ResourceSelectionLocation(Api api, ResourceSelectionModel model) : base(api, model.Enabled)
        {
            Url             = model.Url;
            IconUrl         = model.IconUrl;
            SelectionWidth  = model.SelectionWidth;
            SelectionHeight = model.SelectionHeight;
        }

        public string IconUrl { get; }

        public uint? SelectionHeight { get; }

        public uint? SelectionWidth { get; }

        public string Url { get; }
    }
}