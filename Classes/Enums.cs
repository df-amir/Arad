using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Arad.Classes
{
    public class Enums
    {
        public enum Roles
        {
            Admin = 1,
            SalonOwner = 2,
            TeamOwner = 3,
            Coach = 4,
            Athlete = 5,
            Referee = 6
        }
        public enum Team_Account_Status
        {
            [Display(Name ="در انتظار")]
            Request = 1,

            [Display(Name ="عضو تیم")]
            Join = 2,

            [Display(Name ="اخراج شده")]
            Ban = 3
        }
        public enum Request_Type
        {
            [Display(Name ="دوستانه")]
            Friendly = 1,

            [Display(Name ="رقابتی")]
            Competitive = 2
        }
    }
    public static class PublicFunction
    {
        public static string EnumPersianName(this Enum enumType)
        {
            return enumType.GetType().GetMember(enumType.ToString())
                   .First()
                   .GetCustomAttribute<DisplayAttribute>()
                    .Name;
        }
    }
}