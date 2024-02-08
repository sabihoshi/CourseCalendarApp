using System;
using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.Assignments;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Structures.Assignments
{
    /// <summary>
    ///     Details about an assignment lock.
    /// </summary>
    [PublicAPI]
    public class LockInfo : IPrettyPrint
    {
        private readonly Api _api;

        internal LockInfo(Api api, LockInfoModel model)
        {
            _api           = api;
            AssetString    = model.AssetString;
            UnlockAt       = model.UnlockAt;
            LockAt         = model.LockAt;
            ContextModule  = model.ContextModule;
            ManuallyLocked = model.ManuallyLocked;
        }

        public bool? ManuallyLocked { get; }

        public DateTime? LockAt { get; }

        public DateTime? UnlockAt { get; }

        [CanBeNull] public object ContextModule { get; }

        public string AssetString { get; }

        public string ToPrettyString() => "LockInfo {" +
            ($"\n{nameof(AssetString)}: {AssetString}," +
                $"\n{nameof(UnlockAt)}: {UnlockAt}," +
                $"\n{nameof(LockAt)}: {LockAt}," +
                $"\n{nameof(ContextModule)}: {ContextModule}," +
                $"\n{nameof(ManuallyLocked)}: {ManuallyLocked}").Indent(4) +
            "\n}";
    }
}