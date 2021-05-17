using System;

namespace CallCentre.Common.Extension
{
    public static class DateTimeExtension
    {
        public static string ToDisplayDateFormat(this DateTime value)
        {
            return value.ToString("MM/dd/yyyy HH:mm:ss");
        }
    }
}
