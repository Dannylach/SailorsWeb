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
    public class RentingEquipmentsController : Controller
    {
        private HowDatabaseEntities db = new HowDatabaseEntities();

        // GET: RentingEquipments
        public ActionResult Index()
        {
            var rentingEquipment = db.RentingEquipment.Include(r => r.SailingEquipment).Include(r => r.Users);
            return View(rentingEquipment.ToList());
        }

        // GET: RentingEquipments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentingEquipment rentingEquipment = db.RentingEquipment.Find(id);
            if (rentingEquipment == null)
            {
                return HttpNotFound();
            }
            return View(rentingEquipment);
        }

        // GET: RentingEquipments/Create
        public ActionResult Create()
        {
            ViewBag.equipmentId = new SelectList(db.SailingEquipment, "equipmentId", "equipmentName");
            ViewBag.renterId = new SelectList(db.Users, "userId", "userName");
            return View();
        }

        // POST: RentingEquipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "rentId,renterId,equipmentId,rentTimeStart,rentTimeEnd,rentName,rentDescription")] RentingEquipment rentingEquipment)
        {
            if (ModelState.IsValid)
            {
                db.RentingEquipment.Add(rentingEquipment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.equipmentId = new SelectList(db.SailingEquipment, "equipmentId", "equipmentName", rentingEquipment.equipmentId);
            ViewBag.renterId = new SelectList(db.Users, "userId", "userName", rentingEquipment.renterId);
            return View(rentingEquipment);
        }

        // GET: RentingEquipments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentingEquipment rentingEquipment = db.RentingEquipment.Find(id);
            if (rentingEquipment == null)
            {
                return HttpNotFound();
            }
            ViewBag.equipmentId = new SelectList(db.SailingEquipment, "equipmentId", "equipmentName", rentingEquipment.equipmentId);
            ViewBag.renterId = new SelectList(db.Users, "userId", "userName", rentingEquipment.renterId);
            return View(rentingEquipment);
        }

        // POST: RentingEquipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "rentId,renterId,equipmentId,rentTimeStart,rentTimeEnd,rentName,rentDescription")] RentModel rentingEquipment)
        {
            if (ModelState.IsValid)
            {
                var edited = new RentingEquipment
                {
                    rentId = rentingEquipment.renterId,
                    renterId = rentingEquipment.renterId,
                    equipmentId = rentingEquipment.equipmentId,
                    rentName = rentingEquipment.rentName,
                    rentTimeStart = rentingEquipment.rentTimeStart,
                    rentTimeEnd = rentingEquipment.rentTimeEnd,
                    rentDescription = rentingEquipment.rentDescription
                };
                db.Entry(edited).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.equipmentId = new SelectList(db.SailingEquipment, "equipmentId", "equipmentName", rentingEquipment.equipmentId);
            ViewBag.renterId = new SelectList(db.Users, "userId", "userName", rentingEquipment.renterId);
            return View(rentingEquipment);
        }

        // GET: RentingEquipments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentingEquipment rentingEquipment = db.RentingEquipment.Find(id);
            if (rentingEquipment == null)
            {
                return HttpNotFound();
            }
            return View(rentingEquipment);
        }

        // POST: RentingEquipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RentingEquipment rentingEquipment = db.RentingEquipment.Find(id);
            db.RentingEquipment.Remove(rentingEquipment);
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
