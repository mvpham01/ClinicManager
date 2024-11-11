using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using eProjectP3.Data;
using eProjectP3.Models;

namespace eProjectP3.Controllers
{
    public class medical_applicationController : Controller
    {
        private eProjectP3Context db = new eProjectP3Context();

        // GET: medical_application
        /* public ActionResult Index()
         {
             ViewBag.Categories = new SelectList(db.medical_type, "Medicine_Type_Name", "Medicine_Type_Name");
             var medical_application = db.medical_application.Include(m => m.medical_type);
             return View(medical_application.ToList());
         }*/

        public ActionResult Index(string categoryFilter, string sortOrder, int? page)
        {
            var products = db.medical_application.Include(m => m.medical_type);

            // Apply filters
            if (!string.IsNullOrEmpty(categoryFilter) && categoryFilter != "All Categories")
            {
                products = products.Where(m => m.medical_type.Medicine_Type_Name == categoryFilter);
            }

            // Sorting
            switch (sortOrder)
            {
                case "Name (A-Z)":
                    products = products.OrderBy(m => m.Name);
                    break;
                case "Name (Z-A)":
                    products = products.OrderByDescending(m => m.Name);
                    break;
                case "Cost (Low to High)":
                    products = products.OrderBy(m => m.Cost);
                    break;
                case "Cost (High to Low)":
                    products = products.OrderByDescending(m => m.Cost);
                    break;
                default:

                    products = products.OrderBy(m => m.Name);
                    break;
            }
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            var productsToShow = products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling((double)products.Count() / pageSize);
            ViewBag.Categories = new SelectList(db.medical_type, "Medicine_Type_Name", "Medicine_Type_Name");
            return View(productsToShow);
        }

        // GET: medical_application/Details/5
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

        // GET: medical_application/Create
        public ActionResult Create()
        {
            ViewBag.Medicine_Type_ID = new SelectList(db.medical_type, "Medicine_Type_ID", "Medicine_Type_Name");
            return View();
        }

        // POST: medical_application/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Medicine_ID,Medicine_Type_ID,Img,Cost,Code,Name")] medical_application medical_application)
        {
            if (ModelState.IsValid)
            {
                db.medical_application.Add(medical_application);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Medicine_Type_ID = new SelectList(db.medical_type, "Medicine_Type_ID", "Medicine_Type_Name", medical_application.Medicine_Type_ID);
            return View(medical_application);
        }

        // GET: medical_application/Edit/5
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

        // POST: medical_application/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Medicine_ID,Medicine_Type_ID,Img,Cost,Code,Name")] medical_application medical_application)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medical_application).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Medicine_Type_ID = new SelectList(db.medical_type, "Medicine_Type_ID", "Medicine_Type_Name", medical_application.Medicine_Type_ID);
            return View(medical_application);
        }

        // GET: medical_application/Delete/5
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

        // POST: medical_application/Delete/5
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
