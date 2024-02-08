using System.Collections.Generic;
using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;
using UVACanvasAccess.Model.Modules;
using UVACanvasAccess.Util;

namespace UVACanvasAccess.Structures.Modules
{
    [PublicAPI]
    public class ModuleItemSequence : IPrettyPrint
    {
        private readonly Api _api;

        internal ModuleItemSequence(Api api, ModuleItemSequenceModel items)
        {
            _api  = api;
            Items = items.Items.SelectNotNull(m => new ModuleItemSequenceNode(api, m));
        }

        public IEnumerable<ModuleItemSequenceNode> Items { get; }

        public string ToPrettyString() => "ModuleItemSequence {" +
            $"\n{nameof(Items)}: {Items?.ToPrettyString()}".Indent(4) +
            "\n}";
    }
}