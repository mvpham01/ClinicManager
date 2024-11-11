using eProjectP3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProjectP3.Controllers
{
    public class HomeController : Controller
    {
        private eProjectP3Context db = new eProjectP3Context();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult Search(string searchText)
        {
            var medicalResults = db.medical_application.Where(m => m.Name.Contains(searchText)).ToList();
            var scientificResults = db.scientific_application.Where(s => s.Name.Contains(searchText)).ToList();
            var combinedResults = new List<object>();
            combinedResults.AddRange(medicalResults);
            combinedResults.AddRange(scientificResults);
            ViewBag.SearchResults = combinedResults;
            return View("Index");
        }
    }
}