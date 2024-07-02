using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Arad.Classes;
using Arad.Models;

namespace Arad.Areas.SalonOwner.Controllers
{
    public class SalonController : Controller
    {
        private AradEntities db = new AradEntities();

        public ActionResult Index()
        {
            return View(db.Salon.ToList());
        }

        public ActionResult Pageindex()
        {
            return View(db.Salon.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salon salon = db.Salon.Find(id);
            if (salon == null)
            {
                return HttpNotFound();
            }
            return View(salon);
        }

        public ActionResult Showdetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salon salon = db.Salon.Find(id);
            ViewBag.Salon_Sance = db.Salon_Sance.Where(s => s.SalonId == id)?.ToList();
            if (salon == null)
            {
                return HttpNotFound();
            }
            return View(salon);
        }
        public ActionResult Create()
        {
            var ListPlaqueType = new List<SelectListItem>();
            foreach (Enums.Proviance itemEnum in (Enums.Proviance[])Enum.GetValues(typeof(Enums.Proviance)))
                ListPlaqueType.Add(new SelectListItem() { Value = ((byte)itemEnum).ToString(), Text = itemEnum.EnumPersianName() });
            ViewBag.Province = ListPlaqueType;

            var ListPlaqueType1 = new List<SelectListItem>();
            foreach (Enums.City itemEnum in (Enums.City[])Enum.GetValues(typeof(Enums.City)))
                ListPlaqueType1.Add(new SelectListItem() { Value = ((byte)itemEnum).ToString(), Text = itemEnum.EnumPersianName() });
            ViewBag.City = ListPlaqueType1;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Address,Province,City")] Salon salon)
        {
            if (ModelState.IsValid)
            {
                int? _accountId = null;
                using (AradEntities db = new AradEntities())
                    _accountId = db.Account.FirstOrDefault(q => q.PhoneNumber == User.Identity.Name)?.Id ?? null;

                salon.OwnerAccountId = _accountId;
                db.Salon.Add(salon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var ListPlaqueType = new List<SelectListItem>();
            foreach (Enums.Proviance itemEnum in (Enums.Proviance[])Enum.GetValues(typeof(Enums.Proviance)))
                ListPlaqueType.Add(new SelectListItem() { Value = ((byte)itemEnum).ToString(), Text = itemEnum.EnumPersianName() });
            foreach (var plaqueType in ListPlaqueType)
                if (plaqueType.Value == salon.Province.ToString())
                    plaqueType.Selected = true;
            ViewBag.Province = ListPlaqueType;

            var ListPlaqueType1 = new List<SelectListItem>();
            foreach (Enums.City itemEnum in (Enums.City[])Enum.GetValues(typeof(Enums.City)))
                ListPlaqueType1.Add(new SelectListItem() { Value = ((byte)itemEnum).ToString(), Text = itemEnum.EnumPersianName() });
            foreach (var plaqueType in ListPlaqueType1)
                if (plaqueType.Value == salon.City.ToString())
                    plaqueType.Selected = true;
            ViewBag.City = ListPlaqueType1;

            return View(salon);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salon salon = db.Salon.Find(id);
            if (salon == null)
            {
                return HttpNotFound();
            }

            var ListPlaqueType = new List<SelectListItem>();
            foreach (Enums.Proviance itemEnum in (Enums.Proviance[])Enum.GetValues(typeof(Enums.Proviance)))
                ListPlaqueType.Add(new SelectListItem() { Value = ((byte)itemEnum).ToString(), Text = itemEnum.EnumPersianName() });
            foreach (var plaqueType in ListPlaqueType)
                if (plaqueType.Value == salon.Province.ToString())
                    plaqueType.Selected = true;
            ViewBag.Province = ListPlaqueType;

            var ListPlaqueType1 = new List<SelectListItem>();
            foreach (Enums.City itemEnum in (Enums.City[])Enum.GetValues(typeof(Enums.City)))
                ListPlaqueType1.Add(new SelectListItem() { Value = ((byte)itemEnum).ToString(), Text = itemEnum.EnumPersianName() });
            foreach (var plaqueType in ListPlaqueType1)
                if (plaqueType.Value == salon.City.ToString())
                    plaqueType.Selected = true;
            ViewBag.City = ListPlaqueType1;

            ViewBag.OwnerAccountId = new SelectList(db.Account.Where(q => q.RoleId == (int)Enums.Roles.SalonOwner), "Id", "Person.Name", salon.OwnerAccountId);

            return View(salon);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Address,Province,City,OwnerAccountId")] Salon salon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var ListPlaqueType = new List<SelectListItem>();
            foreach (Enums.Proviance itemEnum in (Enums.Proviance[])Enum.GetValues(typeof(Enums.Proviance)))
                ListPlaqueType.Add(new SelectListItem() { Value = ((byte)itemEnum).ToString(), Text = itemEnum.EnumPersianName() });
            foreach (var plaqueType in ListPlaqueType)
                if (plaqueType.Value == salon.Province.ToString())
                    plaqueType.Selected = true;
            ViewBag.Province = ListPlaqueType;

            var ListPlaqueType1 = new List<SelectListItem>();
            foreach (Enums.City itemEnum in (Enums.City[])Enum.GetValues(typeof(Enums.City)))
                ListPlaqueType1.Add(new SelectListItem() { Value = ((byte)itemEnum).ToString(), Text = itemEnum.EnumPersianName() });
            foreach (var plaqueType in ListPlaqueType1)
                if (plaqueType.Value == salon.City.ToString())
                    plaqueType.Selected = true;
            ViewBag.City = ListPlaqueType1;

            ViewBag.OwnerAccountId = new SelectList(db.Account.Where(q => q.RoleId == (int)Enums.Roles.SalonOwner), "Id", "Person.Name", salon.OwnerAccountId);

            return View(salon);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salon salon = db.Salon.Find(id);
            if (salon == null)
            {
                return HttpNotFound();
            }
            return View(salon);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Salon salon = db.Salon.Find(id);
            db.Salon.Remove(salon);
            db.SaveChanges();
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
