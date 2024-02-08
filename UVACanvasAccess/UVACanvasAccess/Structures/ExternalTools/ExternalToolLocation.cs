using JetBrains.Annotations;
using UVACanvasAccess.ApiParts;

namespace UVACanvasAccess.Structures.ExternalTools
{
    [PublicAPI]
    public abstract class ExternalToolLocation
    {
        private protected Api Api;

        private protected ExternalToolLocation(Api api, bool? enabled)
        {
            Api     = api;
            Enabled = enabled ?? false;
        }

        public bool Enabled { get; }
    }
}