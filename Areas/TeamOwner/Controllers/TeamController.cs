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

namespace Arad.Areas.TeamOwner.Controllers
{
    public class TeamController : Controller
    {
        private AradEntities db = new AradEntities();

        public ActionResult Index()
        {
            var team = db.Team.Include(t => t.Account).Include(t => t.Account1);
            return View(team.ToList());
        }

        public ActionResult Pageindex()
        {
            var team = db.Team.Include(t => t.Account).Include(t => t.Account1);
            return View(team.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Team.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        public ActionResult Showdetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team salon = db.Team.Find(id);
            ViewBag.Team_Account = db.Team_Account.Where(s => s.TeamId == id)?.ToList();
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
            ViewBag.Proviance = ListPlaqueType;

            var ListPlaqueType1 = new List<SelectListItem>();
            foreach (Enums.City itemEnum in (Enums.City[])Enum.GetValues(typeof(Enums.City)))
                ListPlaqueType1.Add(new SelectListItem() { Value = ((byte)itemEnum).ToString(), Text = itemEnum.EnumPersianName() });
            ViewBag.City = ListPlaqueType1;

            ViewBag.CoachAccounId = new SelectList(db.Account.Where(q => q.RoleId == (int)Enums.Roles.Coach), "Id", "Person.Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CreatedDate,Proviance,City,CoachAccounId")] Team team)
        {
            if (ModelState.IsValid)
            {
                int? _accountId = null;
                using (AradEntities db = new AradEntities())
                    _accountId = db.Account.FirstOrDefault(q => q.PhoneNumber == User.Identity.Name)?.Id ?? null;

                team.OwnerAccountId = _accountId;
                db.Team.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var ListPlaqueType = new List<SelectListItem>();
            foreach (Enums.Proviance itemEnum in (Enums.Proviance[])Enum.GetValues(typeof(Enums.Proviance)))
                ListPlaqueType.Add(new SelectListItem() { Value = ((byte)itemEnum).ToString(), Text = itemEnum.EnumPersianName() });
            foreach (var plaqueType in ListPlaqueType)
                if (plaqueType.Value == team.Proviance.ToString())
                    plaqueType.Selected = true;
            ViewBag.Proviance = ListPlaqueType;

            var ListPlaqueType1 = new List<SelectListItem>();
            foreach (Enums.City itemEnum in (Enums.City[])Enum.GetValues(typeof(Enums.City)))
                ListPlaqueType1.Add(new SelectListItem() { Value = ((byte)itemEnum).ToString(), Text = itemEnum.EnumPersianName() });
            foreach (var plaqueType in ListPlaqueType1)
                if (plaqueType.Value == team.City.ToString())
                    plaqueType.Selected = true;
            ViewBag.City = ListPlaqueType1;

            ViewBag.CoachAccounId = new SelectList(db.Account.Where(q => q.RoleId == (int)Enums.Roles.Coach), "Id", "Person.Name", team.CoachAccounId);
            return View(team);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Team.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }

            var ListPlaqueType = new List<SelectListItem>();
            foreach (Enums.Proviance itemEnum in (Enums.Proviance[])Enum.GetValues(typeof(Enums.Proviance)))
                ListPlaqueType.Add(new SelectListItem() { Value = ((byte)itemEnum).ToString(), Text = itemEnum.EnumPersianName() });
            foreach (var plaqueType in ListPlaqueType)
                if (plaqueType.Value == team.Proviance.ToString())
                    plaqueType.Selected = true;
            ViewBag.Proviance = ListPlaqueType;

            var ListPlaqueType1 = new List<SelectListItem>();
            foreach (Enums.City itemEnum in (Enums.City[])Enum.GetValues(typeof(Enums.City)))
                ListPlaqueType1.Add(new SelectListItem() { Value = ((byte)itemEnum).ToString(), Text = itemEnum.EnumPersianName() });
            foreach (var plaqueType in ListPlaqueType1)
                if (plaqueType.Value == team.City.ToString())
                    plaqueType.Selected = true;
            ViewBag.City = ListPlaqueType1;

            ViewBag.OwnerAccountId = new SelectList(db.Account.Where(q => q.RoleId == (int)Enums.Roles.TeamOwner), "Id", "Person.Name", team.OwnerAccountId);
            ViewBag.CoachAccounId = new SelectList(db.Account.Where(q => q.RoleId == (int)Enums.Roles.Coach), "Id", "Person.Name", team.CoachAccounId);
            return View(team);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CreatedDate,Proviance,City,OwnerAccountId,CoachAccounId")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var ListPlaqueType = new List<SelectListItem>();
            foreach (Enums.Proviance itemEnum in (Enums.Proviance[])Enum.GetValues(typeof(Enums.Proviance)))
                ListPlaqueType.Add(new SelectListItem() { Value = ((byte)itemEnum).ToString(), Text = itemEnum.EnumPersianName() });
            foreach (var plaqueType in ListPlaqueType)
                if (plaqueType.Value == team.Proviance.ToString())
                    plaqueType.Selected = true;
            ViewBag.Proviance = ListPlaqueType;

            var ListPlaqueType1 = new List<SelectListItem>();
            foreach (Enums.City itemEnum in (Enums.City[])Enum.GetValues(typeof(Enums.City)))
                ListPlaqueType1.Add(new SelectListItem() { Value = ((byte)itemEnum).ToString(), Text = itemEnum.EnumPersianName() });
            foreach (var plaqueType in ListPlaqueType1)
                if (plaqueType.Value == team.City.ToString())
                    plaqueType.Selected = true;
            ViewBag.City = ListPlaqueType1;

            ViewBag.OwnerAccountId = new SelectList(db.Account.Where(q => q.RoleId == (int)Enums.Roles.TeamOwner), "Id", "Person.Name", team.OwnerAccountId);
            ViewBag.CoachAccounId = new SelectList(db.Account.Where(q => q.RoleId == (int)Enums.Roles.Coach), "Id", "Person.Name", team.CoachAccounId);
            return View(team);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Team.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Team team = db.Team.Find(id);
            db.Team.Remove(team);
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
