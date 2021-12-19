using System;

namespace Candle.Application.System
{
    public static class StringExtensions
    {
        public static short ToShort(this string value)
        {
            return Convert.ToInt16(value);
        }

        public static int ToInt(this string value)
        {
            return Convert.ToInt32(value);
        }

        public static long ToLong(this string value)
        {
            return Convert.ToInt64(value);
        }
    }
}
