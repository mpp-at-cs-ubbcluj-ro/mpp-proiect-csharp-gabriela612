using System;

namespace Utills.utils
{
    public static class DateUtils
    {
        public static DateTime FromString(string dateString)
        {
            DateTime data = DateTime.ParseExact(dateString, "yyyy-MM-dd", null);
            return data;
        }
    }
}