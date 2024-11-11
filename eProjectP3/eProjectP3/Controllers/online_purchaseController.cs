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
    public class online_purchaseController : Controller
    {
        private eProjectP3Context db = new eProjectP3Context();

        // GET: online_purchase
        public ActionResult Index()
        {
            var online_purchases = db.online_purchases.ToList();
            return View(online_purchases);
        }

        // GET: online_purchase/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            online_purchase online_purchase = db.online_purchases.Find(id);
            if (online_purchase == null)
            {
                return HttpNotFound();
            }
            return View(online_purchase);
        }

        // GET: online_purchase/Create
        public ActionResult Create()
        {
            ViewBag.Client_ID = new SelectList(db.clients, "Client_ID", "Clinic_Name");
            return View();
        }

        // POST: online_purchase/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Purchase_ID,Client_ID,Item_ID,Item_Type,Quantity,Total_Amount,Purchase_Date")] online_purchase online_purchase)
        {
            if (ModelState.IsValid)
            {
                db.online_purchases.Add(online_purchase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Client_ID = new SelectList(db.clients, "Client_ID", "Clinic_Name", online_purchase.Client_ID);
            return View(online_purchase);
        }

        // GET: online_purchase/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            online_purchase online_purchase = db.online_purchases.Find(id);
            if (online_purchase == null)
            {
                return HttpNotFound();
            }
            ViewBag.Client_ID = new SelectList(db.clients, "Client_ID", "Clinic_Name", online_purchase.Client_ID);
            return View(online_purchase);
        }

        // POST: online_purchase/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Purchase_ID,Client_ID,Item_ID,Item_Type,Quantity,Total_Amount,Purchase_Date")] online_purchase online_purchase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(online_purchase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Client_ID = new SelectList(db.clients, "Client_ID", "Clinic_Name", online_purchase.Client_ID);
            return View(online_purchase);
        }

        // GET: online_purchase/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            online_purchase online_purchase = db.online_purchases.Find(id);
            if (online_purchase == null)
            {
                return HttpNotFound();
            }
            return View(online_purchase);
        }

        // POST: online_purchase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            online_purchase online_purchase = db.online_purchases.Find(id);
            db.online_purchases.Remove(online_purchase);
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
