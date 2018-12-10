using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SailorsWebApi.Models;

namespace SailorsWebApi.Controllers
{
    public class HabourRentingsController : Controller
    {
        private HowDatabaseEntities db = new HowDatabaseEntities();

        // GET: HabourRentings
        public ActionResult Index()
        {
            var habourRenting = db.HabourRenting.Include(h => h.Users);
            return View(habourRenting.ToList());
        }

        // GET: HabourRentings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HabourRenting habourRenting = db.HabourRenting.Find(id);
            if (habourRenting == null)
            {
                return HttpNotFound();
            }
            return View(habourRenting);
        }

        // GET: HabourRentings/Create
        public ActionResult Create()
        {
            ViewBag.renterId = new SelectList(db.Users, "userId", "userName");
            return View();
        }

        // POST: HabourRentings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "rentId,renterId,rentTimeStart,rentTimeEnd,rentName,rentDescription")] HabourRenting habourRenting)
        {
            if (ModelState.IsValid)
            {
                db.HabourRenting.Add(habourRenting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.renterId = new SelectList(db.Users, "userId", "userName", habourRenting.renterId);
            return View(habourRenting);
        }

        // GET: HabourRentings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HabourRenting habourRenting = db.HabourRenting.Find(id);
            if (habourRenting == null)
            {
                return HttpNotFound();
            }
            ViewBag.renterId = new SelectList(db.Users, "userId", "userName", habourRenting.renterId);
            return View(habourRenting);
        }

        // POST: HabourRentings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "rentId,renterId,rentTimeStart,rentTimeEnd,rentName,rentDescription")] HabourRenting habourRenting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(habourRenting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.renterId = new SelectList(db.Users, "userId", "userName", habourRenting.renterId);
            return View(habourRenting);
        }

        // GET: HabourRentings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HabourRenting habourRenting = db.HabourRenting.Find(id);
            if (habourRenting == null)
            {
                return HttpNotFound();
            }
            return View(habourRenting);
        }

        // POST: HabourRentings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HabourRenting habourRenting = db.HabourRenting.Find(id);
            db.HabourRenting.Remove(habourRenting);
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
