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
    public class RequestController : Controller
    {
        private AradEntities db = new AradEntities();



        public ActionResult Index()
        {
            var request = db.Request.Include(r => r.Account).Include(r => r.Account1).Include(r => r.Team).Include(r => r.Team1);
            return View(request.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Request.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        public ActionResult Create()
        {
            ViewBag.RefereeAccountId = new SelectList(db.Account.Where(q => q.RoleId == (int)Enums.Roles.Referee), "Id", "Person.Name");
            ViewBag.HostTeamId = new SelectList(db.Team, "Id", "Name");
            ViewBag.GuestTeamId = new SelectList(db.Team, "Id", "Name");

            var ListPlaqueType = new List<SelectListItem>();
            foreach (Enums.Request_Type itemEnum in (Enums.Request_Type[])Enum.GetValues(typeof(Enums.Request_Type)))
                ListPlaqueType.Add(new SelectListItem() { Value = ((byte)itemEnum).ToString(), Text = itemEnum.EnumPersianName() });
            ViewBag.Type = ListPlaqueType;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HostTeamId,GuestTeamId,Type,RefereeAccountId")] Request request)
        {
            if (ModelState.IsValid)
            {
                int? _accountId = null;
                using (AradEntities db = new AradEntities())
                    _accountId = db.Account.FirstOrDefault(q => q.PhoneNumber == User.Identity.Name)?.Id ?? null;

                request.CreatorAccountId = _accountId;
                request.Status = false;
                db.Request.Add(request);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RefereeAccountId = new SelectList(db.Account.Where(q => q.RoleId == (int)Enums.Roles.Referee), "Id", "Person.Name", request.RefereeAccountId);
            ViewBag.HostTeamId = new SelectList(db.Team, "Id", "Name", request.HostTeamId);
            ViewBag.GuestTeamId = new SelectList(db.Team, "Id", "Name", request.GuestTeamId);

            var ListPlaqueType = new List<SelectListItem>();
            foreach (Enums.Request_Type itemEnum in (Enums.Request_Type[])Enum.GetValues(typeof(Enums.Request_Type)))
                ListPlaqueType.Add(new SelectListItem() { Value = ((byte)itemEnum).ToString(), Text = itemEnum.EnumPersianName() });
            foreach (var plaqueType in ListPlaqueType)
                if (plaqueType.Value == request.Type.ToString())
                    plaqueType.Selected = true;
            ViewBag.Type = ListPlaqueType;

            return View(request);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Request.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            ViewBag.RefereeAccountId = new SelectList(db.Account.Where(q => q.RoleId == (int)Enums.Roles.Referee), "Id", "Person.Name", request.RefereeAccountId);
            ViewBag.HostTeamId = new SelectList(db.Team, "Id", "Name", request.HostTeamId);
            ViewBag.GuestTeamId = new SelectList(db.Team, "Id", "Name", request.GuestTeamId);

            var ListPlaqueType = new List<SelectListItem>();
            foreach (Enums.Request_Type itemEnum in (Enums.Request_Type[])Enum.GetValues(typeof(Enums.Request_Type)))
                ListPlaqueType.Add(new SelectListItem() { Value = ((byte)itemEnum).ToString(), Text = itemEnum.EnumPersianName() });
            foreach (var plaqueType in ListPlaqueType)
                if (plaqueType.Value == request.Type.ToString())
                    plaqueType.Selected = true;
            ViewBag.Type = ListPlaqueType;

            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HostTeamId,GuestTeamId,Type,RefereeAccountId")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RefereeAccountId = new SelectList(db.Account.Where(q => q.RoleId == (int)Enums.Roles.Referee), "Id", "Person.Name", request.RefereeAccountId);
            ViewBag.HostTeamId = new SelectList(db.Team, "Id", "Name", request.HostTeamId);
            ViewBag.GuestTeamId = new SelectList(db.Team, "Id", "Name", request.GuestTeamId);

            var ListPlaqueType = new List<SelectListItem>();
            foreach (Enums.Request_Type itemEnum in (Enums.Request_Type[])Enum.GetValues(typeof(Enums.Request_Type)))
                ListPlaqueType.Add(new SelectListItem() { Value = ((byte)itemEnum).ToString(), Text = itemEnum.EnumPersianName() });
            foreach (var plaqueType in ListPlaqueType)
                if (plaqueType.Value == request.Type.ToString())
                    plaqueType.Selected = true;
            ViewBag.Type = ListPlaqueType;

            return View(request);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Request.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Request request = db.Request.Find(id);
            db.Request.Remove(request);
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
