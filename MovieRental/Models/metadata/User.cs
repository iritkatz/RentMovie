using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieRental;


namespace MovieRental
{
    public partial class User
    {

        public void Login()
        {
            var coockie = new HttpCookie("UserId");
            coockie.Value = this.id;
            coockie.Expires = DateTime.Now.AddMinutes(90);

            HttpContext.Current.Response.SetCookie(coockie);
        }


        public void logoff()
        {
            if (!(HttpContext.Current.Items["UserId"] == null))
            {
                if (HttpContext.Current.Request.Cookies["UserId"] != null)
                {


                    //  HttpContext.Current.Request.Cookies["UserId"].Expires = DateTime.Now.AddDays(-1d);
                    HttpCookie myCookie = new HttpCookie("UserId");
                    myCookie.Expires = DateTime.Now.AddDays(-1d);
                    HttpContext.Current.Response.Cookies.Add(myCookie);
                }
            }
        }
        public static string CurrentUserId
        {
            get
            {
                if (HttpContext.Current.Items["UserId"] == null)
                {
                    if (HttpContext.Current.Request.Cookies["UserId"] != null)
                    {
                        string id = HttpContext.Current.Request.Cookies["UserId"].Value;
                       
                            HttpContext.Current.Items["UserId"] = id;
                        
                    }
                }

                return HttpContext.Current.Items["UserId"] as string;
            }
        }



    }
}