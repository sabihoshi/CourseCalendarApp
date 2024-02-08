using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.Roles;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Structures.Roles
{
    [PublicAPI]
    public class RolePermissions : IPrettyPrint
    {
        private readonly Api _api;

        internal RolePermissions(Api api, RolePermissionsModel model)
        {
            _api                 = api;
            Enabled              = model.Enabled;
            Locked               = model.Locked;
            AppliesToSelf        = model.AppliesToSelf;
            AppliesToDescendants = model.AppliesToDescendants;
            Readonly             = model.Readonly;
            Explicit             = model.Explicit;
            PriorDefault         = model.PriorDefault;
        }

        public bool AppliesToDescendants { get; }

        public bool AppliesToSelf { get; }

        public bool Enabled { get; }

        public bool Explicit { get; }

        public bool Locked { get; }

        public bool PriorDefault { get; }

        public bool Readonly { get; }

        public string ToPrettyString() => "RolePermissions { " +
            ($"\n{nameof(Enabled)}: {Enabled}," +
                $"\n{nameof(Locked)}: {Locked}," +
                $"\n{nameof(AppliesToSelf)}: {AppliesToSelf}," +
                $"\n{nameof(AppliesToDescendants)}: {AppliesToDescendants}," +
                $"\n{nameof(Readonly)}: {Readonly}," +
                $"\n{nameof(Explicit)}: {Explicit}," +
                $"\n{nameof(PriorDefault)}: {PriorDefault}").Indent(4) +
            "\n}";
    }
}