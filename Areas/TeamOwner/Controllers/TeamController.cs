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
            return View(db.Team.ToList());
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

        public ActionResult Create()
        {
            ViewBag.CoachAccounId = new SelectList(db.Account.Where(q => q.RoleId == (int)Enums.Roles.Coach), "Id", "PhoneNumber");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CreatedDate,Ranc,Proviance,City,CoachAccounId")] Team team)
        {
            if (ModelState.IsValid)
            {
                int _accountId = 0;
                using (AradEntities db = new AradEntities())
                    _accountId = db.Account.FirstOrDefault(q => q.PhoneNumber == User.Identity.Name)?.Id ?? 0;

                team.OwnerAccountId = _accountId;
                db.Team.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CoachAccounId = new SelectList(db.Account.Where(q => q.RoleId == (int)Enums.Roles.Coach), "Id", "PhoneNumber", team.CoachAccounId);

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

            ViewBag.OwnerAccountId = new SelectList(db.Account.Where(q => q.RoleId == (int)Enums.Roles.TeamOwner), "Id", "PhoneNumber", team.OwnerAccountId);
            ViewBag.CoachAccounId = new SelectList(db.Account.Where(q => q.RoleId == (int)Enums.Roles.Coach), "Id", "PhoneNumber", team.CoachAccounId);

            return View(team);
        }

        // POST: TeamOwner/Team/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CreatedDate,Ranc,Proviance,City,OwnerAccountId,CoachAccounId")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OwnerAccountId = new SelectList(db.Account.Where(q => q.RoleId == (int)Enums.Roles.TeamOwner), "Id", "PhoneNumber", team.OwnerAccountId);
            ViewBag.CoachAccounId = new SelectList(db.Account.Where(q => q.RoleId == (int)Enums.Roles.Coach), "Id", "PhoneNumber", team.CoachAccounId);

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
