using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProjectP3.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "admin")
            {
                // Authentication successful, redirect to admin dashboard or perform other admin-related actions
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid username or password. Please try again.";
                return View();
            }
        }

        public ActionResult Dashboard()
        {
            // Admin dashboard logic goes here
            return View();
        }
    }
}