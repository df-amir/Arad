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
    public class Team_AccountController : Controller
    {
        private AradEntities db = new AradEntities();

        public ActionResult Index()
        {
            var team_Account = db.Team_Account.Include(t => t.Account).Include(t => t.Team);
            return View(team_Account.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team_Account team_Account = db.Team_Account.Find(id);
            if (team_Account == null)
            {
                return HttpNotFound();
            }
            return View(team_Account);
        }

        public ActionResult Create()
        {
            var ListPlaqueType = new List<SelectListItem>();
            foreach (Enums.Team_Account_Status itemEnum in (Enums.Team_Account_Status[])Enum.GetValues(typeof(Enums.Team_Account_Status)))
                ListPlaqueType.Add(new SelectListItem() { Value = ((byte)itemEnum).ToString(), Text = itemEnum.EnumPersianName() });
            ViewBag.Status = ListPlaqueType;

            ViewBag.AccountId = new SelectList(db.Account, "Id", "Person.Name");
            ViewBag.TeamId = new SelectList(db.Team, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TeamId,AccountId,Status")] Team_Account team_Account)
        {
            if (ModelState.IsValid)
            {
                db.Team_Account.Add(team_Account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var ListPlaqueType = new List<SelectListItem>();
            foreach (Enums.Team_Account_Status itemEnum in (Enums.Team_Account_Status[])Enum.GetValues(typeof(Enums.Team_Account_Status)))
                ListPlaqueType.Add(new SelectListItem() { Value = ((byte)itemEnum).ToString(), Text = itemEnum.EnumPersianName() });
            foreach (var plaqueType in ListPlaqueType)
                if (plaqueType.Value == team_Account.Status.ToString())
                    plaqueType.Selected = true;
            ViewBag.Status = ListPlaqueType;

            ViewBag.AccountId = new SelectList(db.Account, "Id", "Person.Name", team_Account.AccountId);
            ViewBag.TeamId = new SelectList(db.Team, "Id", "Name", team_Account.TeamId);
            return View(team_Account);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team_Account team_Account = db.Team_Account.Find(id);
            if (team_Account == null)
            {
                return HttpNotFound();
            }

            var ListPlaqueType = new List<SelectListItem>();
            foreach (Enums.Team_Account_Status itemEnum in (Enums.Team_Account_Status[])Enum.GetValues(typeof(Enums.Team_Account_Status)))
                ListPlaqueType.Add(new SelectListItem() { Value = ((byte)itemEnum).ToString(), Text = itemEnum.EnumPersianName() });
            foreach (var plaqueType in ListPlaqueType)
                if (plaqueType.Value == team_Account.Status.ToString())
                    plaqueType.Selected = true;
            ViewBag.Status = ListPlaqueType;

            ViewBag.AccountId = new SelectList(db.Account, "Id", "Person.Name", team_Account.AccountId);
            ViewBag.TeamId = new SelectList(db.Team, "Id", "Name", team_Account.TeamId);
            return View(team_Account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TeamId,AccountId,Status")] Team_Account team_Account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(team_Account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var ListPlaqueType = new List<SelectListItem>();
            foreach (Enums.Team_Account_Status itemEnum in (Enums.Team_Account_Status[])Enum.GetValues(typeof(Enums.Team_Account_Status)))
                ListPlaqueType.Add(new SelectListItem() { Value = ((byte)itemEnum).ToString(), Text = itemEnum.EnumPersianName() });
            foreach (var plaqueType in ListPlaqueType)
                if (plaqueType.Value == team_Account.Status.ToString())
                    plaqueType.Selected = true;
            ViewBag.Status = ListPlaqueType;

            ViewBag.AccountId = new SelectList(db.Account, "Id", "Person.Name", team_Account.AccountId);
            ViewBag.TeamId = new SelectList(db.Team, "Id", "Name", team_Account.TeamId);
            return View(team_Account);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team_Account team_Account = db.Team_Account.Find(id);
            if (team_Account == null)
            {
                return HttpNotFound();
            }
            return View(team_Account);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Team_Account team_Account = db.Team_Account.Find(id);
            db.Team_Account.Remove(team_Account);
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
