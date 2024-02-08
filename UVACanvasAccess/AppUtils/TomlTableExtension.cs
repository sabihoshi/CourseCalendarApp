using System;
using JetBrains.Annotations;
using Tomlyn.Model;

namespace AppUtils
{
    public static class TomlTableExtension
    {
        public static bool Has<T>(this TomlTable table, string key) => table.ContainsKey(key) && table[key] is T;

        public static bool Has(this TomlTable table, string key) => table.ContainsKey(key);

        public static T Get<T>(this TomlTable table, string key) => (T) table[key];

        [Pure]
        public static T GetEnumValue<T>(this TomlTable table, string key, T @default) where T : struct, Enum
        {
            var str = table.Get<string>(key);
            foreach (T field in Enum.GetValues(typeof(T)))
            {
                var representation = field.GetTomlName();
                if (str == representation) return field;
            }
            return @default;
        }

        public static T GetOr<T>(this TomlTable table, string key, T @default = default) where T : struct
        {
            if (!table.ContainsKey(key)) return @default;

            return table[key] as T? ?? @default;
        }

        [CanBeNull]
        public static T MaybeGet<T>(this TomlTable table, string key) where T : class
        {
            if (!table.ContainsKey(key)) return null;

            return table[key] as T;
        }

        [Pure]
        public static T? GetEnumValue<T>(this TomlTable table, string key) where T : struct, Enum
        {
            var str = table.Get<string>(key);
            foreach (T field in Enum.GetValues(typeof(T)))
            {
                var representation = field.GetTomlName();
                if (str == representation) return field;
            }
            return null;
        }

        [CanBeNull]
        [Pure]
        private static string GetTomlName([NotNull] this Enum en)
        {
            var member = en.GetType().GetMember(en.ToString());

            if (member.Length <= 0)
                return null;

            var attribute = member[0].GetCustomAttributes(typeof(TomlEnumValueAttribute), false);

            return attribute.Length > 0
                ? ((TomlEnumValueAttribute) attribute[0]).Name
                : null;
        }
    }
}