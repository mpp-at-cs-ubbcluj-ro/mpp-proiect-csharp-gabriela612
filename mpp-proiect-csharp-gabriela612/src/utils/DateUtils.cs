using System;

namespace Utils
{
    public static class DateUtils
    {
        public static DateOnly FromString(string dateString)
        {
            return DateOnly.ParseExact(dateString, "yyyy-MM-dd",
                System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}