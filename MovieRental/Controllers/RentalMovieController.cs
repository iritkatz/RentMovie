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
    public class RentalMovieController : Controller
    {

        private MovieRentalEntities db = new MovieRentalEntities();
        // GET: RentalMovie
        public ActionResult Index()
        {
            return View(db.RentMovie.Where(a => a.UserId == MovieRental.User.CurrentUserId).ToList());
        }
    }
}