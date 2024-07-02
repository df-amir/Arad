using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Arad.Classes
{
    public class DateTimeConvertor
    {
        public static string ToShamsi(DateTime? value)
        {
            var Result = "";
            if (value != null)
            {
                var DateTime = (DateTime)value;
                PersianCalendar pc = new PersianCalendar();
                return pc.GetYear(DateTime) + "/" + pc.GetMonth(DateTime).ToString("00") + "/" +
                       pc.GetDayOfMonth(DateTime).ToString("00");
            }
            return Result;
        }

        public static DateTime ToMiladi(string PersianDateTime)
        {
            if (string.IsNullOrEmpty(PersianDateTime)) return new DateTime();
            var Date = PersianDateTime?.Split('/', ',', '_', '-', ' ');
            PersianCalendar pc = new PersianCalendar();
            return new DateTime(int.Parse(Date[0]), int.Parse(Date[1]), int.Parse(Date[2]), pc);
        }
    }
}
