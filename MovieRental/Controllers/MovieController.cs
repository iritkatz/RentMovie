using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieRental;
using MovieRental.Models;
using MovieRental.viewModel;

namespace MovieRental.Controllers
{
    public class MovieController : Controller
    {
        private MovieRentalEntities db = new MovieRentalEntities();

        // GET: Movie
        public ActionResult Index()
        {
            var Movie = db.Movie.Include(m => m.Genre);
            return View(Movie.ToList());
        }

        // GET: Movie/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movie.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            if (MovieRental.User.CurrentUserId != null)
            {
                ViewBag.currentRent = db.RentMovie.Where(a => a.MovieId == id && a.Status == "Rented" && MovieRental.User.CurrentUserId == a.UserId).FirstOrDefault();
            }
               
            return View(movie);
        }
        [HttpPost]
        public ActionResult Details(int? movieid,int? returnmovieid)
        {

            if (MovieRental.User.CurrentUserId == null)
                return RedirectToAction("Login", "Home");
            if (movieid != null)
            {
                Movie movie = db.Movie.Find(movieid);
                if (movie.Availbility == 0)
                {
                    return View(movie);
                }
                MovieRental.User myuser = db.User.Where(am => am.id == MovieRental.User.CurrentUserId).FirstOrDefault();

                RentMovie rent = new RentMovie
                {
                    MovieId = (int)movieid,
                    Movie = movie,
                    RentalPrice = movie.price,
                    User = myuser,
                    UserId = myuser.id,
                    StartDate = DateTime.Now,
                    Status = "Rented"


                };
                movie.Availbility--;
                db.RentMovie.Add(rent);
                db.SaveChanges();

                return RedirectToAction("Index", "RentalMovie");
            }else if(returnmovieid != null)
            {
                Movie movie = db.Movie.Find(returnmovieid);
               
                MovieRental.User myuser = db.User.Where(am => am.id == MovieRental.User.CurrentUserId).FirstOrDefault();

                RentMovie rent = db.RentMovie.Where(a => a.MovieId == returnmovieid && a.UserId == myuser.id && a.Status == "Rented").FirstOrDefault();
                movie.Availbility++;
                rent.EndDate = DateTime.Now;
                rent.Status = "Returned";
                db.SaveChanges();

                return RedirectToAction("Index", "RentalMovie");
            }
            return RedirectToAction("Index", "Home");
        }


        // GET: Movie/Create
        public ActionResult Create()
        {
            User aa = db.User.FirstOrDefault(a => a.id == MovieRental.User.CurrentUserId);
            if (aa.Permissions != 2)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.genreID = new SelectList(db.Genre, "id", "Name");
            return View();
        }

        // POST: Movie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movie movie)
        {
            User aa = db.User.FirstOrDefault(a => a.id == MovieRental.User.CurrentUserId);
            if (aa.Permissions != 2)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                db.Movie.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.genreID = new SelectList(db.Genre, "id", "Name", movie.genreID);
            return View(movie);
        }

        // GET: Movie/Edit/5
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
            Movie movie = db.Movie.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            ViewBag.genreID = new SelectList(db.Genre, "id", "Name", movie.genreID);
            return View(movie);
        }

        // POST: Movie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Title,Director,Description,Image,Availbility,genreID,price")] Movie movie)
        {
            User aa = db.User.FirstOrDefault(a => a.id == MovieRental.User.CurrentUserId);
            if (aa.Permissions != 2)
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
            
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.genreID = new SelectList(db.Genre, "id", "Name", movie.genreID);
            return View(movie);
        }

        // GET: Movie/Delete/5
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
            Movie movie = db.Movie.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User aa = db.User.FirstOrDefault(a => a.id == MovieRental.User.CurrentUserId);
            if (aa.Permissions != 2)
            {
                return RedirectToAction("Index", "Home");
            }
            Movie movie = db.Movie.Find(id);
            db.Movie.Remove(movie);
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
