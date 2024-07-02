using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Arad.Models;

namespace Arad.Areas.Referee.Controllers
{
    public class PlayController : Controller
    {
        private AradEntities db = new AradEntities();

        public ActionResult Index()
        {
            var play = db.Play.Include(p => p.Request);
            return View(play.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Play play = db.Play.Find(id);
            if (play == null)
            {
                return HttpNotFound();
            }
            return View(play);
        }

        public ActionResult Showdetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Play play = db.Play.Find(id);
            if (play == null)
            {
                return HttpNotFound();
            }
            return View(play);
        }

        public ActionResult Create()
        {
            ViewBag.RequestId = new SelectList(db.Request.Where(q => q.Status == false).Select(q=>q.Account), "Id", "Person.Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RequestId,HostTeamScore,GuestTeamScore")] Play play)
        {
            if (ModelState.IsValid)
            {
                db.Play.Add(play);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RequestId = new SelectList(db.Request.Where(q => q.Status == false).Select(q => q.Account), "Id", "Person.Name", play.RequestId);
            return View(play);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Play play = db.Play.Find(id);
            if (play == null)
            {
                return HttpNotFound();
            }
            ViewBag.RequestId = new SelectList(db.Request.Where(q => q.Status == false).Select(q => q.Account), "Id", "Person.Name", play.RequestId);
            return View(play);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RequestId,HostTeamScore,GuestTeamScore")] Play play)
        {
            if (ModelState.IsValid)
            {
                db.Entry(play).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RequestId = new SelectList(db.Request.Where(q => q.Status == false).Select(q => q.Account), "Id", "Person.Name", play.RequestId);
            return View(play);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Play play = db.Play.Find(id);
            if (play == null)
            {
                return HttpNotFound();
            }
            return View(play);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Play play = db.Play.Find(id);
            db.Play.Remove(play);
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
