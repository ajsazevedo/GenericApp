using System;

namespace GenericApp.Infra.Common.Extensions
{
    public static class EnumExtensions
    {
        public static TEnum ToEnum<TEnum>(this string value)
        {
            return ToEnum<TEnum>(value, true);
        }

        public static TEnum ToEnum<TEnum>(this string value, bool ignoreCase)
        {
            return (TEnum)Enum.Parse(typeof(TEnum), value, ignoreCase);
        }
    }
}
