using Arad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Arad.Classes;
using System.Security.Cryptography;

namespace Arad.Controllers
{
    public class ReserveController : Controller
    {
        private AradEntities db = new AradEntities();
        public ActionResult Index()
        {
            var reserveList = new List<Reserve>();
            if (User.Identity.IsAuthenticated)
            {
                var _accountId = db.Account.Where(q => q.PhoneNumber == User.Identity.Name)?.FirstOrDefault()?.Id ?? null;
                reserveList = db.Reserve.Where(q => q.AccountId == _accountId).ToList();
            }

            List<object> abgList = new List<object>();
            for (var i = 0; i < 6; i++)
            {
                abgList.Add(new { 
                    value = DateTime.Now.Date.AddDays(i),
                    date = DateTimeConvertor.ToShamsi(DateTime.Now.Date.AddDays(i)).ToString(),
                    dayName = PublicFunction.EnumPersianName((Enums.WeekDays)Enum.Parse(typeof(Enums.WeekDays), DateTime.Now.AddDays(i).DayOfWeek.ToString(), true))
                });
            }
            ViewBag.WeekDays = abgList;

            return View(reserveList);
        }

        [HttpPost]
        public ActionResult GetSansSalonByDate(string date)
        {
            if (string.IsNullOrEmpty(date))
                return View(new List<Salon_Sance>());

            DateTime _date = Convert.ToDateTime(date);

            var salon_Sance = db.Salon_Sance
                .Include(s => s.Salon)
                .Include(s => s.Sance)
                .Where(q => q.Sance.Date == _date)
                .OrderBy(q => q.Sance.StartTime)
                .ToList();

            return View(salon_Sance);
        }

        [HttpPost]
        public string Reserve(int? sanceSalonId)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var salon_Sance = db.Salon_Sance.FirstOrDefault(q => q.Id == sanceSalonId);
                    if (salon_Sance != null)
                    {
                        if (salon_Sance.Status == true)
                        {
                            salon_Sance.Status = false;
                            db.Entry(salon_Sance).State = EntityState.Modified;

                            var _accountId = db.Account.Where(q => q.PhoneNumber == User.Identity.Name)?.FirstOrDefault()?.Id ?? null;

                            var _finalPrice = salon_Sance.Sance.Price;
                            if (salon_Sance.Sance.Discount > 0)
                                _finalPrice = salon_Sance.Sance.Price - ((salon_Sance.Sance.Price * salon_Sance.Sance.Discount) / 100);

                            Random _r = new Random();
                            var _token = _r.Next(10000000, 99999999).ToString();
                            var oldTokenList = db.Reserve.Select(q => q.Token).ToList();

                            while (oldTokenList.Contains(_token))
                            {
                                _token = _r.Next(10000000, 99999999).ToString();
                            }

                            db.Reserve.Add(new Reserve
                            {
                                SalonSanceId = salon_Sance.Id,
                                AccountId = _accountId,
                                Date = DateTime.Now,
                                Token = _token,
                                IsDeleted = false,
                                FinalPrice = (int)_finalPrice
                            });

                            db.SaveChanges();

                            return "سانس با موفقیت برای شما رزرو شد، کد رزرو شما: " + _token;

                        }
                        else
                            return "متاسفانه این سانس رزرو شده است";
                    }
                    else
                        return "سانس نا معتبر می‌باشد";
                }
                else
                {
                    return "لطفا قبل از رزرو وارد حساب کاربری خود شوید";
                }
            }
            catch
            {
                return "فرآیند با خطا مواجه شد";
            }
        }
    }
}