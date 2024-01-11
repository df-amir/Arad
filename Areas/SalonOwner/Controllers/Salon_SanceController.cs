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
    public class Salon_SanceController : Controller
    {
        private AradEntities db = new AradEntities();

        public ActionResult Index()
        {
            var salon_Sance = db.Salon_Sance.Include(s => s.Salon).Include(s => s.Sance);
            return View(salon_Sance.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salon_Sance salon_Sance = db.Salon_Sance.Find(id);
            if (salon_Sance == null)
            {
                return HttpNotFound();
            }
            return View(salon_Sance);
        }

        public ActionResult Create()
        {
            ViewBag.SalonId = new SelectList(db.Salon, "Id", "Title");
            ViewBag.SanceId = new SelectList(db.Sance, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SalonId,SanceId,Status")] Salon_Sance salon_Sance)
        {
            if (ModelState.IsValid)
            {
                db.Salon_Sance.Add(salon_Sance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SalonId = new SelectList(db.Salon, "Id", "Title", salon_Sance.SalonId);
            ViewBag.SanceId = new SelectList(db.Sance, "Id", "Id", salon_Sance.SanceId);
            return View(salon_Sance);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salon_Sance salon_Sance = db.Salon_Sance.Find(id);
            if (salon_Sance == null)
            {
                return HttpNotFound();
            }
            ViewBag.SalonId = new SelectList(db.Salon, "Id", "Title", salon_Sance.SalonId);
            ViewBag.SanceId = new SelectList(db.Sance, "Id", "Id", salon_Sance.SanceId);
            return View(salon_Sance);
        }

        // POST: SalonOwner/Salon_Sance/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SalonId,SanceId,Status")] Salon_Sance salon_Sance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salon_Sance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SalonId = new SelectList(db.Salon, "Id", "Title", salon_Sance.SalonId);
            ViewBag.SanceId = new SelectList(db.Sance, "Id", "Id", salon_Sance.SanceId);
            return View(salon_Sance);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salon_Sance salon_Sance = db.Salon_Sance.Find(id);
            if (salon_Sance == null)
            {
                return HttpNotFound();
            }
            return View(salon_Sance);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Salon_Sance salon_Sance = db.Salon_Sance.Find(id);
            db.Salon_Sance.Remove(salon_Sance);
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
