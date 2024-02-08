using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UVACanvasAccess.Model.Enrollments;

namespace UVACanvasAccess.Model.Users
{
    internal class UserModel
    {
        [JsonProperty("id")] public ulong Id { get; set; }

        [JsonProperty("last_login")] public DateTime? LastLogin { get; set; }

        [JsonProperty("permissions")] public Dictionary<string, bool> Permissions { get; set; }

        [JsonProperty("enrollments")] public List<EnrollmentModel> Enrollments { get; set; }

        [JsonProperty("avatar_url")] public string AvatarUrl { get; set; }

        [JsonProperty("bio")] public string Bio { get; set; }

        [JsonProperty("effective_locale")] public string EffectiveLocale { get; set; }

        [JsonProperty("email")] public string Email { get; set; }

        [JsonProperty("integration_id")] public string IntegrationId { get; set; }

        [JsonProperty("locale")] public string Locale { get; set; }

        [JsonProperty("login_id")] public string LoginId { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("short_name")] public string ShortName { get; set; }

        [JsonProperty("sis_user_id")] public string SisUserId { get; set; }

        [JsonProperty("sortable_name")] public string SortableName { get; set; }

        [JsonProperty("time_zone")] public string TimeZone { get; set; }

        [JsonProperty("sis_import_id")] public ulong? SisImportId { get; set; }
    }
}