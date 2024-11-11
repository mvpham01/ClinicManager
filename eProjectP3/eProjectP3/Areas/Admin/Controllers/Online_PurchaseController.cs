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
    public class Online_PurchaseController : Controller
    {
        private eProjectP3Context db = new eProjectP3Context();

        // GET: Admin/Online_Purchase
        public ActionResult Index()
        {
            var online_purchase_details = db.online_purchase_details.Include(o => o.MedicalApplication).Include(o => o.OnlinePurchase).Include(o => o.ScientificApplication);
            return View(online_purchase_details.ToList());
        }

        // GET: Admin/Online_Purchase/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            online_purchase_detail online_purchase_detail = db.online_purchase_details.Find(id);
            if (online_purchase_detail == null)
            {
                return HttpNotFound();
            }
            return View(online_purchase_detail);
        }

        // GET: Admin/Online_Purchase/Create
        public ActionResult Create()
        {
            ViewBag.Item_ID = new SelectList(db.medical_application, "Medicine_ID", "Img");
            ViewBag.Purchase_ID = new SelectList(db.online_purchases, "Purchase_ID", "Purchase_ID");
            ViewBag.Item_ID = new SelectList(db.scientific_application, "Apparatus_ID", "Img");
            return View();
        }

        // POST: Admin/Online_Purchase/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Purchase_Detail_ID,Purchase_ID,Item_ID,Item_Type,Quantity")] online_purchase_detail online_purchase_detail)
        {
            if (ModelState.IsValid)
            {
                db.online_purchase_details.Add(online_purchase_detail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Item_ID = new SelectList(db.medical_application, "Medicine_ID", "Img", online_purchase_detail.Item_ID);
            ViewBag.Purchase_ID = new SelectList(db.online_purchases, "Purchase_ID", "Purchase_ID", online_purchase_detail.Purchase_ID);
            ViewBag.Item_ID = new SelectList(db.scientific_application, "Apparatus_ID", "Img", online_purchase_detail.Item_ID);
            return View(online_purchase_detail);
        }

        // GET: Admin/Online_Purchase/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            online_purchase_detail online_purchase_detail = db.online_purchase_details.Find(id);
            if (online_purchase_detail == null)
            {
                return HttpNotFound();
            }
            ViewBag.Item_ID = new SelectList(db.medical_application, "Medicine_ID", "Img", online_purchase_detail.Item_ID);
            ViewBag.Purchase_ID = new SelectList(db.online_purchases, "Purchase_ID", "Purchase_ID", online_purchase_detail.Purchase_ID);
            ViewBag.Item_ID = new SelectList(db.scientific_application, "Apparatus_ID", "Img", online_purchase_detail.Item_ID);
            return View(online_purchase_detail);
        }

        // POST: Admin/Online_Purchase/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Purchase_Detail_ID,Purchase_ID,Item_ID,Item_Type,Quantity")] online_purchase_detail online_purchase_detail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(online_purchase_detail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Item_ID = new SelectList(db.medical_application, "Medicine_ID", "Img", online_purchase_detail.Item_ID);
            ViewBag.Purchase_ID = new SelectList(db.online_purchases, "Purchase_ID", "Purchase_ID", online_purchase_detail.Purchase_ID);
            ViewBag.Item_ID = new SelectList(db.scientific_application, "Apparatus_ID", "Img", online_purchase_detail.Item_ID);
            return View(online_purchase_detail);
        }

        // GET: Admin/Online_Purchase/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            online_purchase_detail online_purchase_detail = db.online_purchase_details.Find(id);
            if (online_purchase_detail == null)
            {
                return HttpNotFound();
            }
            return View(online_purchase_detail);
        }

        // POST: Admin/Online_Purchase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            online_purchase_detail online_purchase_detail = db.online_purchase_details.Find(id);
            db.online_purchase_details.Remove(online_purchase_detail);
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
