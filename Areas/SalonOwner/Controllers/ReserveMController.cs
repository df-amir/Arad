using Arad.Classes;
using Arad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Arad.Areas.SalonOwner.Controllers
{
    public class ReserveMController : Controller
    {
        private AradEntities db = new AradEntities();
        public ActionResult Index()
        {
            var reserveList = new List<Reserve>();
            if (User.Identity.IsAuthenticated)
            {
                var _accountId = db.Account.Where(q => q.PhoneNumber == User.Identity.Name)?.FirstOrDefault()?.Id ?? null;
                var salonSonceIdList = db.Salon.Where(q => q.OwnerAccountId == _accountId)?.SelectMany(q=>q.Salon_Sance.Select(w=>w.Id))?.ToList() ?? new List<int>();
                reserveList = db.Reserve.Where(q => salonSonceIdList.Contains((int)q.SalonSanceId)).ToList();
            }

            return View(reserveList);
        }
    }
}