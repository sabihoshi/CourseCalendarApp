using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UVACanvasAccess.Model.Assignments;
using UVACanvasAccess.Model.Users;

namespace UVACanvasAccess.Model.Pages
{
    internal class PageModel
    {
        [JsonProperty("front_page")] public bool FrontPage { get; set; }

        [JsonProperty("locked_for_user")] public bool LockedForUser { get; set; }

        [JsonProperty("published")] public bool Published { get; set; }

        [JsonProperty("created_at")] public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")] public DateTime UpdatedAt { get; set; }

        [JsonProperty("lock_info")]
        [CanBeNull]
        public LockInfoModel LockInfo { get; set; }

        [JsonProperty("body")] [CanBeNull] public string Body { get; set; }

        [JsonProperty("editing_roles")] public string EditingRoles { get; set; }

        [JsonProperty("lock_explanation")]
        [CanBeNull]
        public string LockExplanation { get; set; }

        [JsonProperty("page_id")] public string PageId { get; set; }

        [JsonProperty("title")] public string Title { get; set; }

        [JsonProperty("url")] public string Url { get; set; }

        [JsonProperty("last_edited_by")] public UserDisplayModel LastEditedBy { get; set; }
    }
}