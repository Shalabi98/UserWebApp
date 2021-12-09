using System;
using System.Globalization;

namespace Cultures.Extensions
{
    public static class ConvertTimezoneExtension
    {
        public static DateTime ToLocalTime(this DateTime dateTime, string timeZoneId)
        {
            var destinationTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, destinationTimeZone);
        }
        public static string ToFormat(this DateTime dateTime, string culture)
        {
            var cultureInfo = CultureInfo.GetCultureInfo(culture);
            return dateTime.ToString(cultureInfo);
        }
    }
}
