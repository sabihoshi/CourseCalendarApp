using System.Collections.Generic;
using Newtonsoft.Json;
using UVACanvasAccess.Model.Accounts;

namespace UVACanvasAccess.Model.Roles
{
    internal class RoleModel
    {
        [JsonProperty("account")] public AccountModel Account { get; set; }

        [JsonProperty("permissions")] public Dictionary<string, RolePermissionsModel> Permissions { get; set; }

        [JsonProperty("base_role_type")] public string BaseRoleType { get; set; }

        [JsonProperty("label")] public string Label { get; set; }

        [JsonProperty("workflow_state")] public string WorkflowState { get; set; }
    }
}