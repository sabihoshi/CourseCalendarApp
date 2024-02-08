using System;
using JetBrains.Annotations;

namespace AppUtils
{
    [AttributeUsage(AttributeTargets.Field)]
    [PublicAPI]
    public sealed class TomlEnumValueAttribute : Attribute
    {
        public TomlEnumValueAttribute(string name) { Name = name; }

        public string Name { get; }
    }
}