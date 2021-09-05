using System.Text.Json;

namespace GenericApp.Infra.Common.Extensions
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string source) =>
            source[0].ToString().ToLower() + (source.Length > 1 ? source[1..] : "");
        public static string ToTitleCase(this string source) =>
            source[0].ToString().ToUpper() + (source.Length > 1 ? source[1..] : "");
        public static long ToLong(this string source) => source.ToInt64();
        public static bool IsNullOrEmpty(this string source) => string.IsNullOrEmpty(source);
        public static bool IsNullOrWhiteSpace(this string source) => string.IsNullOrWhiteSpace(source);
        public static T Deserialize<T>(this string _obj)
        {
            if (_obj is null || string.IsNullOrWhiteSpace(_obj))
                return (T)(object)null;

            return JsonSerializer.Deserialize<T>(_obj);
        }
    }
}
