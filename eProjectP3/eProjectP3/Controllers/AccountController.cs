using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using eProjectP3.Data;
using eProjectP3.Models;


namespace eProjectP3.Controllers
{
    public class AccountController : Controller
    {
        private eProjectP3Context db = new eProjectP3Context();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(client model)
        {
            var user = db.clients.SingleOrDefault(u => u.Clinic_Name == model.Clinic_Name && u.Clinic_Password == model.Clinic_Password);

            if (user != null)
            {
                Session["Client_ID"] = user.Client_ID;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt");
                return View(model);
            }
        }

        // GET: Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(client model)
        {
            if (ModelState.IsValid)
            {
                // Check for null or empty values in the model before saving
                if (string.IsNullOrWhiteSpace(model.Clinic_Name) ||
                    string.IsNullOrWhiteSpace(model.Clinic_Password) ||
                    string.IsNullOrWhiteSpace(model.Contact_Person) ||
                    string.IsNullOrWhiteSpace(model.Address) ||
                    string.IsNullOrWhiteSpace(model.Phone) ||
                    string.IsNullOrWhiteSpace(model.Email))
                {
                    ModelState.AddModelError(string.Empty, "All fields are required.");
                    return View(model);
                }

                // Validate email using a regular expression
                string emailRegex = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
                if (!System.Text.RegularExpressions.Regex.IsMatch(model.Email, emailRegex))
                {
                    ModelState.AddModelError("Email", "Invalid email format.");
                    return View(model);
                }
                if (db.clients.Any(c => c.Clinic_Name == model.Clinic_Name))
                {
                    ModelState.AddModelError("Clinic_Name", "Username is already taken.");
                    return View(model);
                }

                // Validate phone number using a regular expression
                string phoneRegex = @"^\d{10}$"; // Assuming a 10-digit phone number
                if (!System.Text.RegularExpressions.Regex.IsMatch(model.Phone, phoneRegex))
                {
                    ModelState.AddModelError("Phone", "Invalid phone number format (10 digits).");
                    return View(model);
                }

                // If all validations pass, save the model
                db.clients.Add(model);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(model);
        }


        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult EditProfile()
        {
            var clientId = Session["Client_ID"];
            if (clientId == null)
            {
                return RedirectToAction("Login");
            }

            int clientIdInt = Convert.ToInt32(clientId);
            var client = db.clients.Find(clientIdInt);

            if (client == null)
            {
                return HttpNotFound();
            }

            return View(client);
        }

        // POST: Account/EditProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(client client)
        {
            int clientIdFromSession = (int)Session["Client_ID"];
            client.Client_ID = clientIdFromSession;

            if (string.IsNullOrWhiteSpace(client.Clinic_Name) ||
                string.IsNullOrWhiteSpace(client.Clinic_Password) ||
                string.IsNullOrWhiteSpace(client.Contact_Person) ||
                string.IsNullOrWhiteSpace(client.Address) ||
                string.IsNullOrWhiteSpace(client.Phone) ||
                string.IsNullOrWhiteSpace(client.Email))
            {
                ModelState.AddModelError(string.Empty, "All fields are required.");
                return View(client);
            }

            // Validate email using a regular expression
            string emailRegex = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(client.Email, emailRegex))
            {
                ModelState.AddModelError("Email", "Invalid email format.");
                return View(client);
            }
            if (db.clients.Any(c => c.Clinic_Name == client.Clinic_Name))
            {
                ModelState.AddModelError("Clinic_Name", "Username is already taken.");
                return View(client);
            }

            // Validate phone number using a regular expression
            string phoneRegex = @"^\d{10}$"; // Assuming a 10-digit phone number
            if (!System.Text.RegularExpressions.Regex.IsMatch(client.Phone, phoneRegex))
            {
                ModelState.AddModelError("Phone", "Invalid phone number format (10 digits).");
                return View(client);
            }

            try
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                ModelState.AddModelError(string.Empty, "The record you attempted to edit was modified by another user. The edit operation was canceled and the current values in the database have been displayed.");
            }

            return View(client);
        }
    }
}