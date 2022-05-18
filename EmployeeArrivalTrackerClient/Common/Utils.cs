using System;
using System.Globalization;

namespace Common
{
    public static class Utils
    {
        public static DateTime GetCurrentDate()
        {
            string strDate = DateTime.Now.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
            DateTime date;

            string[] formats = { "yyyy-MM-dd HH:mm:ss", "yyyy.MM.dd HH:mm:ss" };

            if (DateTime.TryParseExact(strDate,
                                       formats,
                                       CultureInfo.InvariantCulture,
                                       DateTimeStyles.None,
                                       out date))
            {
                return date;
            }

            return DateTime.Now;
        }
    }
}
