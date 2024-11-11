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

namespace eProjectP3.Controllers
{
    public class medical_typeController : Controller
    {
        private eProjectP3Context db = new eProjectP3Context();

        // GET: medical_type
        public ActionResult Index()
        {
            return View(db.medical_type.ToList());
        }

        // GET: medical_type/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            medical_type medical_type = db.medical_type.Find(id);
            if (medical_type == null)
            {
                return HttpNotFound();
            }
            return View(medical_type);
        }

        // GET: medical_type/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: medical_type/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Medicine_Type_ID,Medicine_Type_Name")] medical_type medical_type)
        {
            if (ModelState.IsValid)
            {
                db.medical_type.Add(medical_type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(medical_type);
        }

        // GET: medical_type/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            medical_type medical_type = db.medical_type.Find(id);
            if (medical_type == null)
            {
                return HttpNotFound();
            }
            return View(medical_type);
        }

        // POST: medical_type/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Medicine_Type_ID,Medicine_Type_Name")] medical_type medical_type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medical_type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(medical_type);
        }

        // GET: medical_type/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            medical_type medical_type = db.medical_type.Find(id);
            if (medical_type == null)
            {
                return HttpNotFound();
            }
            return View(medical_type);
        }

        // POST: medical_type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            medical_type medical_type = db.medical_type.Find(id);
            db.medical_type.Remove(medical_type);
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
