using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Cms.Tests
{
    public class UtilsTest
    {
        public static DateTime AddDateAndHours(DateTime date, int days, int hours)
        {
            hours = hours > 23 ? 23 : hours;
            string strHours = hours < 9 ? "0" + hours : hours + "";
            string dateFormat = date.AddDays(days).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + " " + hours + ":00:00";
            return Convert.ToDateTime(dateFormat);
        }
    }
}
