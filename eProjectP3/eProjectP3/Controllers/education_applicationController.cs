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
    public class education_applicationController : Controller
    {
        private eProjectP3Context db = new eProjectP3Context();

        // GET: education_application
        public ActionResult Index()
        {
            var education_application = db.education_application.Include(e => e.staff);
            return View(education_application.ToList());
        }

        // GET: education_application/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            education_application education_application = db.education_application.Find(id);
            if (education_application == null)
            {
                return HttpNotFound();
            }
            return View(education_application);
        }

        // GET: education_application/Create
        public ActionResult Create()
        {
            ViewBag.Staff_ID = new SelectList(db.staffs, "Staff_ID", "Name");
            return View();
        }

        // POST: education_application/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Education_ID,Subject,Type,Staff_ID,Date,Time")] education_application education_application)
        {
            if (ModelState.IsValid)
            {
                db.education_application.Add(education_application);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Staff_ID = new SelectList(db.staffs, "Staff_ID", "Name", education_application.Staff_ID);
            return View(education_application);
        }

        // GET: education_application/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            education_application education_application = db.education_application.Find(id);
            if (education_application == null)
            {
                return HttpNotFound();
            }
            ViewBag.Staff_ID = new SelectList(db.staffs, "Staff_ID", "Name", education_application.Staff_ID);
            return View(education_application);
        }

        // POST: education_application/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Education_ID,Subject,Type,Staff_ID,Date,Time")] education_application education_application)
        {
            if (ModelState.IsValid)
            {
                db.Entry(education_application).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Staff_ID = new SelectList(db.staffs, "Staff_ID", "Name", education_application.Staff_ID);
            return View(education_application);
        }

        // GET: education_application/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            education_application education_application = db.education_application.Find(id);
            if (education_application == null)
            {
                return HttpNotFound();
            }
            return View(education_application);
        }

        // POST: education_application/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            education_application education_application = db.education_application.Find(id);
            db.education_application.Remove(education_application);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult StaffDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            staff staff = db.staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
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
