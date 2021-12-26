using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryWebApplication.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LibraryWebApplication.Controllers
{
    public class GenreController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Genre
        public ActionResult Index(string sortOrder, string searchString)
        {
            if (User.Identity.IsAuthenticated)
            {
                //var user = User.Identity;
                //ViewBag.Name = user.Name;

                //ViewBag.displayMenu = "No";

                //if (isAdminUser())
                //{
                //    ViewBag.displayMenu = "Yes";
                //}
                ViewBag.GenreNameSortParm = sortOrder == "genreName" ? "genreName_desc" : "genreName";

                var genres = from d in db.Genres
                             select d;

                // filter
                if (!String.IsNullOrEmpty(searchString))
                {
                    genres = genres.Where(d => d.GenreName.Contains(searchString));
                }

                // order
                switch (sortOrder)
                {
                    case "genreName":
                        genres = genres.OrderBy(d => d.GenreName);
                        break;
                    case "genreName_desc":
                        genres = genres.OrderByDescending(d => d.GenreName);
                        break;
                    default:
                        genres = genres.OrderBy(d => d.GenreId);
                        break;
                }
                return View(genres.ToList());
            }
            else
            {
                //ViewBag.Name = "Not Logged IN";
                return RedirectToAction("Index", "Home");
            }
        }

        public Boolean isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Admin")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        // GET: Genre/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // GET: Genre/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genre/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GenreId,GenreName")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                db.Genres.Add(genre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(genre);
        }

        // GET: Genre/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // POST: Genre/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GenreId,GenreName")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(genre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genre);
        }

        // GET: Genre/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // POST: Genre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Genre genre = db.Genres.Find(id);
            db.Genres.Remove(genre);
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
