using eProjectP3.Data;
using eProjectP3.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace eProjectP3.Controllers
{
    public class feedbacksController : Controller
    {
        private eProjectP3Context db = new eProjectP3Context();

        // GET: feedbacks
        public ActionResult Index()
        {
            {
                var feedbacks = db.feedbacks.Include(f => f.client);
                return View(feedbacks.ToList());
            }
        }

        // GET: feedbacks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            feedback feedback = db.feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // GET: feedbacks/Create
        /*public ActionResult Create()
        {
            ViewBag.Client_ID = new SelectList(db.clients, "Client_ID", "Clinic_Name");
            return View();
        }

        // POST: feedbacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Feedback_Text")] feedback feedback)
        {
            if (Session["Client_ID"] != null)
            {
                feedback.Client_ID = (int?)Session["Client_ID"];

                if (ModelState.IsValid)
                {
                    feedback.Date = DateTime.Now.AddTicks(-(DateTime.Now.Ticks % TimeSpan.TicksPerSecond));
                    db.feedbacks.Add(feedback);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ViewBag.ErrorMessage = "You need to be logged in to leave feedback.";
            }

            return View(feedback);
        }*/
        public ActionResult Create()
        {
            ViewBag.Client_ID = new SelectList(db.clients, "Client_ID", "Clinic_Name");
            return View();
        }

        // POST: feedbacks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Feedback_Text")] feedback feedback)
        {
            if (Session["Client_ID"] != null)
            {
                feedback.Client_ID = (int?)Session["Client_ID"];

                if (ModelState.IsValid)
                {
                    feedback.Date = DateTime.Now.AddTicks(-(DateTime.Now.Ticks % TimeSpan.TicksPerSecond));
                    db.feedbacks.Add(feedback);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ViewBag.ErrorMessage = "You need to be logged in to leave feedback.";
            }

            // Reload feedbacks for the view
            var feedbacks = db.feedbacks.Include(f => f.client).ToList();
            return View("Index", feedbacks);
        }
        // GET: feedbacks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            feedback feedback = db.feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            ViewBag.Client_ID = new SelectList(db.clients, "Client_ID", "Clinic_Name", feedback.Client_ID);
            return View(feedback);
        }

        // POST: feedbacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Feedback_ID,Client_ID,Feedback_Text,Date")] feedback feedback)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feedback).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Client_ID = new SelectList(db.clients, "Client_ID", "Clinic_Name", feedback.Client_ID);
            return View(feedback);
        }

        // GET: feedbacks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            feedback feedback = db.feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            if (feedback != null)
            {
                db.feedbacks.Remove(feedback);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index");
        }
        [HttpPost]
        public ActionResult UpdateFeedbackText(int feedbackId, string updatedText)
        {
            var feedback = db.feedbacks.Find(feedbackId);
            if (feedback != null)
            {
                feedback.Feedback_Text = updatedText;
                db.SaveChanges();
            }

            // You can return JSON or any other response as needed
            return Json(new { success = true });
        }
        [HttpPost]
        public ActionResult EditFeedbackText(int feedbackId, string editedText)
        {
            var feedback = db.feedbacks.Find(feedbackId);
            if (feedback != null)
            {
                feedback.Feedback_Text = editedText;
                db.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });
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
