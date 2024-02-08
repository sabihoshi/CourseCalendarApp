using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.Users;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Structures.Users
{
    [PublicAPI]
    public class UserDisplay : IPrettyPrint
    {
        private readonly Api _api;

        internal UserDisplay(Api api, UserDisplayModel model)
        {
            _api           = api;
            ShortName      = model.ShortName;
            AvatarImageUrl = model.AvatarImageUrl;
            HtmlUrl        = model.HtmlUrl;
            DisplayName    = model.DisplayName;
        }

        public string AvatarImageUrl { get; }

        public string DisplayName { get; }

        public string HtmlUrl { get; }

        public string ShortName { get; }

        public string ToPrettyString() => "UserDisplay {" +
            ($"\n{nameof(ShortName)}: {ShortName}," +
                $"\n{nameof(DisplayName)}: {DisplayName}," +
                $"\n{nameof(AvatarImageUrl)}: {AvatarImageUrl}," +
                $"\n{nameof(HtmlUrl)}: {HtmlUrl}").Indent(4) +
            "\n}";
    }
}