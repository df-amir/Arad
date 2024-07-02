using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Arad.Models;

namespace Arad.Areas.SalonOwner.Controllers
{
    public class SanceController : Controller
    {
        private AradEntities db = new AradEntities();

        public ActionResult Index()
        {
            return View(db.Sance.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sance sance = db.Sance.Find(id);
            if (sance == null)
            {
                return HttpNotFound();
            }
            return View(sance);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,StartTime,EndTime,Price,Discount")] Sance sance)
        {
            if (ModelState.IsValid)
            {
                db.Sance.Add(sance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sance);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sance sance = db.Sance.Find(id);
            if (sance == null)
            {
                return HttpNotFound();
            }
            return View(sance);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,StartTime,EndTime,Price,Discount")] Sance sance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sance);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sance sance = db.Sance.Find(id);
            if (sance == null)
            {
                return HttpNotFound();
            }
            return View(sance);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sance sance = db.Sance.Find(id);
            db.Sance.Remove(sance);
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
