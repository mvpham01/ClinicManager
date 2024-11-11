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
    public class scientific_applicationController : Controller
    {
        private eProjectP3Context db = new eProjectP3Context();

        // GET: scientific_application
        public ActionResult Index()
        {
            return View(db.scientific_application.ToList());
        }

        // GET: scientific_application/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            scientific_application scientific_application = db.scientific_application.Find(id);
            if (scientific_application == null)
            {
                return HttpNotFound();
            }
            return View(scientific_application);
        }

        // GET: scientific_application/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: scientific_application/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Apparatus_ID,Img,Cost,Description,Name")] scientific_application scientific_application)
        {
            if (ModelState.IsValid)
            {
                db.scientific_application.Add(scientific_application);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(scientific_application);
        }

        // GET: scientific_application/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            scientific_application scientific_application = db.scientific_application.Find(id);
            if (scientific_application == null)
            {
                return HttpNotFound();
            }
            return View(scientific_application);
        }

        // POST: scientific_application/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Apparatus_ID,Img,Cost,Description,Name")] scientific_application scientific_application)
        {
            if (ModelState.IsValid)
            {
                db.Entry(scientific_application).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(scientific_application);
        }

        // GET: scientific_application/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            scientific_application scientific_application = db.scientific_application.Find(id);
            if (scientific_application == null)
            {
                return HttpNotFound();
            }
            return View(scientific_application);
        }

        // POST: scientific_application/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            scientific_application scientific_application = db.scientific_application.Find(id);
            db.scientific_application.Remove(scientific_application);
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
