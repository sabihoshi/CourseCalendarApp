using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.ExternalTools;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Structures.ExternalTools
{
    [PublicAPI]
    public class CourseNavigationLocation : ExternalToolLocation, IToolText, IToolVisibility, IToolDisplayType
    {
        internal CourseNavigationLocation(Api api, CourseNavigationModel model) : base(api, model.Enabled)
        {
            Text         = model.Text;
            Visibility   = model.Visibility.ToApiRepresentedEnum<ToolVisibility>().Expect();
            DisplayType  = model.DisplayType.ToApiRepresentedEnum<ToolDisplayType>().Expect();
            WindowTarget = model.WindowTarget.ToApiRepresentedEnum<ToolWindowTarget>().Expect();
            Default      = model.Default;
        }

        public bool? Default { get; }

        public ToolWindowTarget WindowTarget { get; }

        public ToolDisplayType DisplayType { get; }

        public string Text { get; }

        public ToolVisibility Visibility { get; }
    }
}