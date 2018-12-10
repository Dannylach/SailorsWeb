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
    public class ScoutRanksController : Controller
    {
        private HowDatabaseEntities db = new HowDatabaseEntities();

        // GET: ScoutRanks
        public ActionResult Index()
        {
            return View(db.ScoutRanks.ToList());
        }

        // GET: ScoutRanks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScoutRanks scoutRanks = db.ScoutRanks.Find(id);
            if (scoutRanks == null)
            {
                return HttpNotFound();
            }
            return View(scoutRanks);
        }

        // GET: ScoutRanks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ScoutRanks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "scoutRankId,scoutRankName")] ScoutRanks scoutRanks)
        {
            if (ModelState.IsValid)
            {
                db.ScoutRanks.Add(scoutRanks);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(scoutRanks);
        }

        // GET: ScoutRanks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScoutRanks scoutRanks = db.ScoutRanks.Find(id);
            if (scoutRanks == null)
            {
                return HttpNotFound();
            }
            return View(scoutRanks);
        }

        // POST: ScoutRanks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "scoutRankId,scoutRankName")] ScoutRanks scoutRanks)
        {
            if (ModelState.IsValid)
            {
                db.Entry(scoutRanks).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(scoutRanks);
        }

        // GET: ScoutRanks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScoutRanks scoutRanks = db.ScoutRanks.Find(id);
            if (scoutRanks == null)
            {
                return HttpNotFound();
            }
            return View(scoutRanks);
        }

        // POST: ScoutRanks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ScoutRanks scoutRanks = db.ScoutRanks.Find(id);
            db.ScoutRanks.Remove(scoutRanks);
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
