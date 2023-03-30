using BooksMgtSysWA.Models;
using BooksMgtSysWA.Models.SupportClasses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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

        // GET: Books/Details/5
        //modified to give better view -- VJ
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            string sts = book.status.ToString();
            string bnum = book.booking_num;
            bool isRes = false;

            if (sts == "RS")
            {
                sts = "Reserved";
                isRes = true;
            }
            else
            {
                sts = "Not Reserved";
            }

            if (bnum == "" || bnum == null)
            {
                bnum = "--";
            }

            ViewBag.sts = sts;
            ViewBag.isRes = isRes;
            ViewBag.bnum = bnum;
            SelectedBook.selBookID = book.id.ToString();
            return View(book);
        }

        // POST: Books/BookRes/{selcted book}
        // to update book status -- VJ
        [HttpPost, ActionName("BookRes")]
        [ValidateAntiForgeryToken]
        public ActionResult BookRes()
        {
            Book book = db.Books.Find("" + SelectedBook.selBookID); ;

            if (ModelState.IsValid)
            {
                book.status = "RS";
                book.booking_num = "" + book.id.ToString() + "C001";
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}