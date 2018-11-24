using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieRental.viewModel;
using MovieRental.Excretions;
using MovieRental.Models;
using Movierental.ViewModels;

namespace MovieRental.Controllers
{
    public class HomeController : Controller
    {
        private MovieRentalEntities db = new MovieRentalEntities();
        public ActionResult Index()
        {
            var thumbnils = new List<ThumbnailModel>().GetMovieThumbnai(new MovieRentalEntities());
            var count = thumbnils.Count() / 4;
            var model = new List<thumbnailboxViewModel>();
            for(int i=0;i<=count; i++)
            {
                model.Add(new thumbnailboxViewModel
                {
                    
                   Thumbnail = thumbnils.Skip(i * 4).Take(4)
                });
            }


            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult AboutAs()
        {
            User aa = db.User.FirstOrDefault(a => a.id == MovieRental.User.CurrentUserId);
          
            return View(aa);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            else
            {
                using (var db = new MovieRentalEntities())
                {
                    var existUser = db.User.Where(f => f.id.Equals(user.username, StringComparison.InvariantCultureIgnoreCase) && f.Password == user.password).FirstOrDefault();

                    if (existUser != null)
                    {
                        existUser.Login();
                        return RedirectToAction("Index", "Home");

                    }
                }

                ModelState.AddModelError("Password", "האימייל או הסיסמא אינם נכונים");
                return View(user);
            }
        }

        public ActionResult Logoff()
        {
            if ( MovieRental.User.CurrentUserId != "")
            {
                MovieRentalEntities db = new MovieRentalEntities(); db.Configuration.LazyLoadingEnabled = false;
                MovieRental.User aa = db.User.FirstOrDefault(a => a.id == MovieRental.User.CurrentUserId);
                aa.logoff();
            }

            return RedirectToAction("Login", "Home");
        }

        public ActionResult Register()
        {
            
         
          
           
               
            return View();
        }
        [HttpPost]
        public ActionResult Register(MovieRental.User user)
        {
           
            if (!ModelState.IsValid)
            {
               
                return View(user);
            }

            MovieRentalEntities db = new MovieRentalEntities();
            var existUser = db.User.Where(f => f.id == user.id).FirstOrDefault();

            if (existUser != null)
            {
                ModelState.AddModelError("Username", "השם משתמש כבר קיים במערכת");
                return View(user);
            }

            user.Permissions = 1;
            db.User.Add(user);
            db.SaveChanges();

            return RedirectToAction("Login");
        }

    }
}