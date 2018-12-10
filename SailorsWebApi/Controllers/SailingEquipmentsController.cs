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
    public class SailingEquipmentsController : Controller
    {
        private HowDatabaseEntities db = new HowDatabaseEntities();

        // GET: SailingEquipments
        public ActionResult Index()
        {
            var sailingEquipment = db.SailingEquipment.Include(s => s.EquipmentTypes);
            return View(sailingEquipment.ToList());
        }

        // GET: SailingEquipments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SailingEquipment sailingEquipment = db.SailingEquipment.Find(id);
            if (sailingEquipment == null)
            {
                return HttpNotFound();
            }
            return View(sailingEquipment);
        }

        // GET: SailingEquipments/Create
        public ActionResult Create()
        {
            ViewBag.equipmentType = new SelectList(db.EquipmentTypes, "typeId", "typeName");
            return View();
        }

        // POST: SailingEquipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "equipmentId,equipmentName,equipmentType")] SailingEquipment sailingEquipment)
        {
            if (ModelState.IsValid)
            {
                db.SailingEquipment.Add(sailingEquipment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.equipmentType = new SelectList(db.EquipmentTypes, "typeId", "typeName", sailingEquipment.equipmentType);
            return View(sailingEquipment);
        }

        // GET: SailingEquipments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SailingEquipment sailingEquipment = db.SailingEquipment.Find(id);
            if (sailingEquipment == null)
            {
                return HttpNotFound();
            }
            ViewBag.equipmentType = new SelectList(db.EquipmentTypes, "typeId", "typeName", sailingEquipment.equipmentType);
            return View(sailingEquipment);
        }

        // POST: SailingEquipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "equipmentId,equipmentName,equipmentType")] SailingEquipment sailingEquipment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sailingEquipment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.equipmentType = new SelectList(db.EquipmentTypes, "typeId", "typeName", sailingEquipment.equipmentType);
            return View(sailingEquipment);
        }

        // GET: SailingEquipments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SailingEquipment sailingEquipment = db.SailingEquipment.Find(id);
            if (sailingEquipment == null)
            {
                return HttpNotFound();
            }
            return View(sailingEquipment);
        }

        // POST: SailingEquipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SailingEquipment sailingEquipment = db.SailingEquipment.Find(id);
            db.SailingEquipment.Remove(sailingEquipment);
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
