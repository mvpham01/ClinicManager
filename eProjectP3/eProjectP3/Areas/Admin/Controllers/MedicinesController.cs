using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eProjectP3.Data;
using eProjectP3.Models;
using System.IO;

namespace eProjectP3.Areas.Admin.Controllers
{
    public class MedicinesController : Controller
    {
        private eProjectP3Context db = new eProjectP3Context();

        // GET: Admin/Medicines
        public ActionResult Index()
        {
            var medical_application = db.medical_application.Include(m => m.medical_type);
            return View(medical_application.ToList());
        }

        // GET: Admin/Medicines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            medical_application medical_application = db.medical_application.Find(id);
            if (medical_application == null)
            {
                return HttpNotFound();
            }
            return View(medical_application);
        }

        // GET: Admin/Medicines/Create
        public ActionResult Create()
        {
            ViewBag.Medicine_Type_ID = new SelectList(db.medical_type, "Medicine_Type_ID", "Medicine_Type_Name");
            ViewBag.Medicine_Code = new SelectList(db.medical_application, "Medicine_ID", "Code");
            return View();
        }

        // POST: Admin/Medicines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Medicine_ID,Medicine_Type_ID,Img,Cost,Code,Name")] medical_application medical_application, HttpPostedFileBase uploadFile)
        {
            if (uploadFile != null)
            {
                string fileName = medical_application.Name + ".jpg";
                string path = Path.Combine(Server.MapPath("~/Img/"), fileName);

                uploadFile.SaveAs(path);
                medical_application.Img = fileName;
            }

            if (ModelState.IsValid)
            {
                db.medical_application.Add(medical_application);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.Medicine_Type_ID = new SelectList(db.medical_type, "Medicine_Type_ID", "Medicine_Type_Name", medical_application.Medicine_Type_ID);
            ViewBag.Medicine_Code = new SelectList(db.medical_application, "Medicine_ID", "Code", medical_application.Medicine_ID);
            return View(medical_application);
        }

        // GET: Admin/Medicines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            medical_application medical_application = db.medical_application.Find(id);
            if (medical_application == null)
            {
                return HttpNotFound();
            }
            ViewBag.Medicine_Type_ID = new SelectList(db.medical_type, "Medicine_Type_ID", "Medicine_Type_Name", medical_application.Medicine_Type_ID);
            return View(medical_application);
        }

        // POST: Admin/Medicines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Medicine_ID,Medicine_Type_ID,Img,Cost,Code,Name")] medical_application medical_application, HttpPostedFileBase uploadFile)
        {
            if (uploadFile != null)
            {
                string fileName = medical_application.Name + ".jpg";
                string path = Path.Combine(Server.MapPath("~/Img/"), fileName);

                uploadFile.SaveAs(path);
                medical_application.Img = fileName;
            }

            if (ModelState.IsValid)
            {
                db.Entry(medical_application).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Medicine_Type_ID = new SelectList(db.medical_type, "Medicine_Type_ID", "Medicine_Type_Name", medical_application.Medicine_Type_ID);
            return View(medical_application);
        }

        // GET: Admin/Medicines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            medical_application medical_application = db.medical_application.Find(id);
            if (medical_application == null)
            {
                return HttpNotFound();
            }
            return View(medical_application);
        }

        // POST: Admin/Medicines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            medical_application medical_application = db.medical_application.Find(id);
            db.medical_application.Remove(medical_application);
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
