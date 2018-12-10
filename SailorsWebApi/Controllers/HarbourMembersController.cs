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
    public class HarbourMembersController : Controller
    {
        private HowDatabaseEntities db = new HowDatabaseEntities();

        // GET: HarbourMembers
        public ActionResult Index()
        {
            var harbourMembers = db.HarbourMembers.Include(h => h.Users).Include(h => h.ScoutRanks);
            return View(harbourMembers.ToList());
        }

        // GET: HarbourMembers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HarbourMembers harbourMembers = db.HarbourMembers.Find(id);
            if (harbourMembers == null)
            {
                return HttpNotFound();
            }
            return View(harbourMembers);
        }

        // GET: HarbourMembers/Create
        public ActionResult Create()
        {
            ViewBag.userId = new SelectList(db.Users, "userId", "userName");
            ViewBag.userScoutRank = new SelectList(db.ScoutRanks, "scoutRankId", "scoutRankName");
            return View();
        }

        // POST: HarbourMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "memberId,userId,userScoutRank,userBirthDate,userScoutNumber")] HarbourMembers harbourMembers)
        {
            if (ModelState.IsValid)
            {
                db.HarbourMembers.Add(harbourMembers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userId = new SelectList(db.Users, "userId", "userName", harbourMembers.userId);
            ViewBag.userScoutRank = new SelectList(db.ScoutRanks, "scoutRankId", "scoutRankName", harbourMembers.userScoutRank);
            return View(harbourMembers);
        }

        // GET: HarbourMembers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HarbourMembers harbourMembers = db.HarbourMembers.Find(id);
            if (harbourMembers == null)
            {
                return HttpNotFound();
            }
            ViewBag.userId = new SelectList(db.Users, "userId", "userName", harbourMembers.userId);
            ViewBag.userScoutRank = new SelectList(db.ScoutRanks, "scoutRankId", "scoutRankName", harbourMembers.userScoutRank);
            return View(harbourMembers);
        }

        // POST: HarbourMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "memberId,userId,userScoutRank,userBirthDate,userScoutNumber")] HarbourMembers harbourMembers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(harbourMembers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userId = new SelectList(db.Users, "userId", "userName", harbourMembers.userId);
            ViewBag.userScoutRank = new SelectList(db.ScoutRanks, "scoutRankId", "scoutRankName", harbourMembers.userScoutRank);
            return View(harbourMembers);
        }

        // GET: HarbourMembers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HarbourMembers harbourMembers = db.HarbourMembers.Find(id);
            if (harbourMembers == null)
            {
                return HttpNotFound();
            }
            return View(harbourMembers);
        }

        // POST: HarbourMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HarbourMembers harbourMembers = db.HarbourMembers.Find(id);
            db.HarbourMembers.Remove(harbourMembers);
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
