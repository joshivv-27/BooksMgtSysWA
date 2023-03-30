using BooksMgtSysWA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BooksMgtSysWA.Controllers
{
    public class HomeController : Controller
    {
        private BooksDBEntities db = new BooksDBEntities();

        // GET: Books
        //to filter data
        public ActionResult Index(string searchString)
        {
            var data = db.Books.ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                data = data.Where(d => d.name.ToLower().Contains(searchString.ToLower())).ToList();
            }

            return View(data);
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
    }
}