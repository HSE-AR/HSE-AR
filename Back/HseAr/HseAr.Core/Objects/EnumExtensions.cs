#nullable enable

using System;
using System.Linq;

namespace Afisha.Tickets.Core.Objects
{
    public static class EnumExtensions
    {
        public static T ConvertTo<T>(this Enum value) where T : struct, Enum
            => (T)Enum.Parse(typeof(T), value.ToString());

        public static T? ConvertToNullable<T>(this Enum? value)
            where T : struct, Enum
            => value != null
                ? (T?)Enum.Parse(typeof(T), value.ToString())
                : default;

        public static TAttribute? GetAttribute<TAttribute>(this Enum value)
            where TAttribute : Attribute
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);

            return type.GetField(name)
                .GetCustomAttributes(false)
                .OfType<TAttribute>()
                .SingleOrDefault();
        }
    }
}
