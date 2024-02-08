using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.ExternalTools;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Structures.ExternalTools
{
    [PublicAPI]
    public class AccountNavigationLocation : ExternalToolLocation, IToolUrl, IToolText, IToolSelectionDimensions,
        IToolDisplayType
    {
        internal AccountNavigationLocation(Api api, AccountNavigationModel model) : base(api, model.Enabled)
        {
            Url             = model.Url;
            Text            = model.Text;
            SelectionWidth  = model.SelectionWidth;
            SelectionHeight = model.SelectionHeight;
            DisplayType     = model.DisplayType.ToApiRepresentedEnum<ToolDisplayType>() ?? ToolDisplayType.Default;
        }

        public ToolDisplayType DisplayType { get; }

        public uint? SelectionHeight { get; }

        public uint? SelectionWidth { get; }

        public string Text { get; }

        public string Url { get; }
    }
}