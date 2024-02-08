using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.Users;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Structures.Users
{
    /// <summary>
    ///     A short representation of a user.
    /// </summary>
    [PublicAPI]
    public class ShortUser : IPrettyPrint
    {
        private readonly Api _api;

        internal ShortUser(Api api, ShortUserModel model)
        {
            _api           = api;
            Id             = model.Id;
            DisplayName    = model.DisplayName;
            AvatarImageUrl = model.AvatarImageUrl;
            HtmlUrl        = model.HtmlUrl;
        }

        public ulong Id { get; }

        public string AvatarImageUrl { get; }

        public string DisplayName { get; }

        public string HtmlUrl { get; }

        public string ToPrettyString() => "ShortUser {" +
            ($"\n{nameof(Id)}: {Id}," +
                $"\n{nameof(DisplayName)}: {DisplayName}," +
                $"\n{nameof(AvatarImageUrl)}: {AvatarImageUrl}," +
                $"\n{nameof(HtmlUrl)}: {HtmlUrl}").Indent(4) +
            "\n}";
    }
}