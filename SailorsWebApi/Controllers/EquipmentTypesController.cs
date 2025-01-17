﻿using System;
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
    public class EquipmentTypesController : Controller
    {
        private HowDatabaseEntities db = new HowDatabaseEntities();

        // GET: EquipmentTypes
        public ActionResult Index()
        {
            return View(db.EquipmentTypes.ToList());
        }

        // GET: EquipmentTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentTypes equipmentTypes = db.EquipmentTypes.Find(id);
            if (equipmentTypes == null)
            {
                return HttpNotFound();
            }
            return View(equipmentTypes);
        }

        // GET: EquipmentTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EquipmentTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "typeId,typeName,maxUsers")] EquipmentTypes equipmentTypes)
        {
            if (ModelState.IsValid)
            {
                db.EquipmentTypes.Add(equipmentTypes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(equipmentTypes);
        }

        // GET: EquipmentTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentTypes equipmentTypes = db.EquipmentTypes.Find(id);
            if (equipmentTypes == null)
            {
                return HttpNotFound();
            }
            return View(equipmentTypes);
        }

        // POST: EquipmentTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "typeId,typeName,maxUsers")] EquipmentTypes equipmentTypes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipmentTypes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(equipmentTypes);
        }

        // GET: EquipmentTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentTypes equipmentTypes = db.EquipmentTypes.Find(id);
            if (equipmentTypes == null)
            {
                return HttpNotFound();
            }
            return View(equipmentTypes);
        }

        // POST: EquipmentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EquipmentTypes equipmentTypes = db.EquipmentTypes.Find(id);
            db.EquipmentTypes.Remove(equipmentTypes);
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
