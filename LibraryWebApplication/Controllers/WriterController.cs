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
    public class WriterController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Writer
        public ActionResult Index(string sortOrder, string searchString)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.FirstNameSortParm = sortOrder == "firstName" ? "firstName_desc" : "firstName";

                var writers = from d in db.Writers
                              select d;

                // filter
                if (!String.IsNullOrEmpty(searchString))
                {
                    writers = writers.Where(d => d.LastName.Contains(searchString));
                }

                // order
                switch (sortOrder)
                {
                    case "firstName":
                        writers = writers.OrderBy(d => d.FirstName);
                        break;
                    case "firstName_desc":
                        writers = writers.OrderByDescending(d => d.FirstName);
                        break;
                    default:
                        writers = writers.OrderBy(d => d.WriterId);
                        break;
                }
                return View(writers.ToList());
            } else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Writer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Writer writer = db.Writers.Find(id);
            if (writer == null)
            {
                return HttpNotFound();
            }
            return View(writer);
        }

        // GET: Writer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Writer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WriterId,FirstName,LastName,UserName")] Writer writer)
        {
            if (ModelState.IsValid)
            {
                db.Writers.Add(writer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(writer);
        }

        // GET: Writer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Writer writer = db.Writers.Find(id);
            if (writer == null)
            {
                return HttpNotFound();
            }
            return View(writer);
        }

        // POST: Writer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WriterId,FirstName,LastName,UserName")] Writer writer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(writer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(writer);
        }

        // GET: Writer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Writer writer = db.Writers.Find(id);
            if (writer == null)
            {
                return HttpNotFound();
            }
            return View(writer);
        }

        // POST: Writer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Writer writer = db.Writers.Find(id);
            db.Writers.Remove(writer);
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
