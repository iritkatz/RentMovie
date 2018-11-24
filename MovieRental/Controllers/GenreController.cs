using MovieRental.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MovieRental.Controllers
{
    public class GenreController : Controller
    {
        private MovieRentalEntities db;

        public GenreController()
        {
            db = new MovieRentalEntities();
        }

        // GET: Genre
        public ActionResult Index()
        {
            User aa = db.User.FirstOrDefault(a => a.id == MovieRental.User.CurrentUserId);
            if (aa.Permissions != 2)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(db.Genre.ToList());
        }

        //Get 
        public ActionResult Create()
        {
            User aa = db.User.FirstOrDefault(a => a.id == MovieRental.User.CurrentUserId);
            if (aa.Permissions != 2)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(Genre genre)
        {
            User aa = db.User.FirstOrDefault(a => a.id == MovieRental.User.CurrentUserId);
            if (aa.Permissions != 2)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                db.Genre.Add(genre);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            User aa = db.User.FirstOrDefault(a => a.id == MovieRental.User.CurrentUserId);
            if (aa.Permissions != 2)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Genre genre = db.Genre.Find(id);
            if (genre == null)
            {
                return HttpNotFound();

            }
       
            return View(genre);
        }

        public ActionResult Edit(int? id)
        {
            User aa = db.User.FirstOrDefault(a => a.id == MovieRental.User.CurrentUserId);
            if (aa.Permissions != 2)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Genre genre = db.Genre.Find(id);
            if (genre == null)
            {
                return HttpNotFound();

            }

            return View(genre);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Genre genre)

        {
            User aa = db.User.FirstOrDefault(a => a.id == MovieRental.User.CurrentUserId);
            if (aa.Permissions != 2)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {

                //var GenreId = db.Genre.FirstOrDefault(n => n.id.Equals(genre.id));
                //GenreId.Name = genre.Name;

                db.Entry(genre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int? id)
        {
            User aa = db.User.FirstOrDefault(a => a.id == MovieRental.User.CurrentUserId);
            if (aa.Permissions != 2)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Genre genre = db.Genre.Find(id);
            if (genre == null)
            {
                return HttpNotFound();

            }

            return View(genre);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)


        {
            User aa = db.User.FirstOrDefault(a => a.id == MovieRental.User.CurrentUserId);
            if (aa.Permissions != 2)
            {
                return RedirectToAction("Index", "Home");
            }

            Genre genre = db.Genre.Find(id);
            db.Genre.Remove(genre);
            db.SaveChanges();
            return RedirectToAction("Index");


        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
    }
}