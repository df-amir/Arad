using Arad.Classes;
using Arad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Net;

namespace Arad.Controllers
{
    public class HomeController : Controller
    {
        private AradEntities db = new AradEntities();
        public ActionResult Index()
        {
            ViewBag.listLastPlay=db.Play.ToList()?.OrderByDescending(x => x.Id)?.Take(4)?.ToList();
            ViewBag.listTeam = db.Team.ToList();
            ViewBag.listSalon = db.Salon.ToList();
            return View("Index");
        }

        [HttpPost]
        public ActionResult Register([Bind(Include = "Id,PhoneNumber,Password,RoleId")] Account account)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(account?.PhoneNumber) || string.IsNullOrEmpty(account?.Password))
                        ModelState.AddModelError(nameof(account.PhoneNumber), "پر کردن شماره موبایل و رمز عبور اجباری می‌باشد!");

                    var _account = db.Account.Where(q => q.PhoneNumber == account.PhoneNumber)?.FirstOrDefault();
                    if (_account == null)
                    {
                        account.IsActive = true;
                        db.Account.Add(account);
                        db.SaveChanges();
                        FormsAuthentication.SetAuthCookie(account.PhoneNumber, true);
                        return RedirectToAction("Index");
                    }
                    else
                        ModelState.AddModelError(nameof(account.PhoneNumber), "این شماره موبایل پیش از این استفاده شده است!");
                }

                var ListPlaqueType = new List<SelectListItem>();
                foreach (Enums.Roles itemEnum in (Enums.Roles[])Enum.GetValues(typeof(Enums.Roles)))
                    ListPlaqueType.Add(new SelectListItem() { Value = ((byte)itemEnum).ToString(), Text = itemEnum.EnumPersianName() });
                foreach (var plaqueType in ListPlaqueType)
                    if (plaqueType.Value == account.RoleId.ToString())
                        plaqueType.Selected = true;
                ViewBag.RoleId = ListPlaqueType;

                return View(account);
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login([Bind(Include = "Id,PhoneNumber,Password,RoleId")] Account account)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (account != null)
                    {
                        string _phoneNumber = account.PhoneNumber?.Trim();
                        string _Password = account.Password;
                        var _account = db.Account.Where(q => q.PhoneNumber.Trim() == _phoneNumber && q.Password == _Password)?.FirstOrDefault();
                        if (_account != null)
                        {
                            FormsAuthentication.SetAuthCookie(account.PhoneNumber, true);
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError(nameof(account.PhoneNumber), "کاربری با این اطلاعات یافت نشد!");
                        }
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}