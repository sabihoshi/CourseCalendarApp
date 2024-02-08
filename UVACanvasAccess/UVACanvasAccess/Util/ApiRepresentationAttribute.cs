using System;

namespace UVACanvasAccess.Util
{
    [AttributeUsage(AttributeTargets.Field)]
    internal sealed class ApiRepresentationAttribute : Attribute
    {
        public ApiRepresentationAttribute(string representation) { Representation = representation; }

        internal string Representation { get; }
    }
}