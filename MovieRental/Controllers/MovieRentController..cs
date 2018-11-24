using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieRental.Controllers
{
    public class MovieRent : Controller
    {
        private MovieRentalEntities db;
        // GET: MovieRent 

        public MovieRent()
        {
            db = new MovieRentalEntities();
        }

        public ActionResult Create()
        {

            return View();
        }
        public ActionResult Index()
        {

            return View(db.RentMovie.Where(a =>a.UserId == MovieRental.User.CurrentUserId).ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }
    }
}