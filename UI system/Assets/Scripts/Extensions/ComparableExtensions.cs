using System.Collections.Generic;

namespace Extensions
{
    public static class ComparableExtensions
    {
        public static bool IsDefault<T>(this T _value) where T : struct
        {
            return (!EqualityComparer<T>.Default.Equals(_value, default(T)));
        }

        public static bool IsNull<T>(this T _value) where T : class
        {
            return _value == null;
        }
    }
}