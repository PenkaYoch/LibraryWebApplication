using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryWebApplication.Models;

namespace LibraryWebApplication.Controllers
{
    public class BookController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Book
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.WriterSortParm = sortOrder == "writer" ? "writer_desc" : "writer";

            var books = from d in db.Books
                        select d;

            // filter
            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(d => d.Title.Contains(searchString)
                                       || d.Description.Contains(searchString));
            }

            // order
            switch (sortOrder)
            {
                case "title":
                    books = books.OrderBy(d => d.Title);
                    break;
                case "title_desc":
                    books = books.OrderByDescending(d => d.Title);
                    break;
                case "writer":
                    books = books.OrderBy(d => d.BookWriter.LastName);
                    break;
                case "writer_desc":
                    books = books.OrderByDescending(d => d.BookWriter.LastName);
                    break;
                default:
                    books = books.OrderBy(d => d.Id);
                    break;
            }

            return View(books.ToList());
        }

        // GET: Book/Details/5
        public ActionResult Details(int? id)
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
            return View(book);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "GenreName");
            ViewBag.WriterId = new SelectList(db.Writers, "WriterId", "FirstName");
            return View();
        }

        // POST: Book/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,ReleaseDate,WriterId,GenreId,Description")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "GenreName", book.GenreId);
            ViewBag.WriterId = new SelectList(db.Writers, "WriterId", "FirstName", book.WriterId);
            return View(book);
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "GenreName", book.GenreId);
            ViewBag.WriterId = new SelectList(db.Writers, "WriterId", "FirstName", book.WriterId);
            return View(book);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,ReleaseDate,WriterId,GenreId,Description")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "GenreName", book.GenreId);
            ViewBag.WriterId = new SelectList(db.Writers, "WriterId", "FirstName", book.WriterId);
            return View(book);
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int? id)
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
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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
