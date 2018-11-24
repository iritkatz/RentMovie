using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MovieRental.Controllers
{
    public class UserController : Controller
    {
        private MovieRentalEntities db = new MovieRentalEntities();
        private object[] id;

        // GET: User

        public ActionResult Index()
        {
            User aa = db.User.FirstOrDefault(a => a.id == MovieRental.User.CurrentUserId);
            if (aa.Permissions != 2)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(db.User.ToList());
        }
        public ActionResult Edit(string id)
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

            User user = db.User.Find(id);
            if (user == null)
            {
               
                return HttpNotFound();

            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)

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

               db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }



        public ActionResult Delete(string id)
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

            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();

            }

            return View(user);
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

            User user = db.User.Find(id);
            db.User.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");


        }


        public ActionResult Rents(string id)
        {
            User aa = db.User.FirstOrDefault(a => a.id == MovieRental.User.CurrentUserId);
            if (aa.Permissions != 2)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(db.RentMovie.Where(a=> a.UserId == id).ToList());
        }
        public ActionResult EditUser()
        {
            User aa = db.User.FirstOrDefault(a => a.id == MovieRental.User.CurrentUserId);
            
          
            return View(aa);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(User user)
        {

            {
                User aa = db.User.FirstOrDefault(a => a.id == MovieRental.User.CurrentUserId);
                

                if (ModelState.IsValid)
                {

                    //var GenreId = db.Genre.FirstOrDefault(n => n.id.Equals(genre.id));
                    //GenreId.Name = genre.Name;

                    //  db.Entry(aa).State = EntityState.Added;
                    aa.LastName = user.LastName;
                    aa.Password = user.Password;
                    aa.Permissions = user.Permissions;
                    aa.FirstName = user.FirstName;
                    aa.Email = user.Email;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View();
            }



        }
    }
    }
