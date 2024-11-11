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

namespace eProjectP3.Areas.Admin.Controllers
{
    public class BusinessController : Controller
    {
        private eProjectP3Context db = new eProjectP3Context();

        // GET: Admin/Business
        public ActionResult Index()
        {
            return View(db.business_application.ToList());
        }

        // GET: Admin/Business/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            business_application business_application = db.business_application.Find(id);
            if (business_application == null)
            {
                return HttpNotFound();
            }
            return View(business_application);
        }

        // GET: Admin/Business/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Business/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Transaction_ID,Sector,Amount,Date")] business_application business_application)
        {
            if (ModelState.IsValid)
            {
                db.business_application.Add(business_application);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(business_application);
        }

        // GET: Admin/Business/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            business_application business_application = db.business_application.Find(id);
            if (business_application == null)
            {
                return HttpNotFound();
            }
            return View(business_application);
        }

        // POST: Admin/Business/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Transaction_ID,Sector,Amount,Date")] business_application business_application)
        {
            if (ModelState.IsValid)
            {
                db.Entry(business_application).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(business_application);
        }

        // GET: Admin/Business/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            business_application business_application = db.business_application.Find(id);
            if (business_application == null)
            {
                return HttpNotFound();
            }
            return View(business_application);
        }

        // POST: Admin/Business/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            business_application business_application = db.business_application.Find(id);
            db.business_application.Remove(business_application);
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
