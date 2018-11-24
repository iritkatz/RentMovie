using MovieRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieRental.Excretions
{
    public static class Thumbnail_excretions
    {
        private static object m;

        public static IEnumerable<ThumbnailModel> GetMovieThumbnai(this List<ThumbnailModel> thumbnails, MovieRentalEntities db = null)
        {
            try {
                if (db == null)
                {
                    db = new MovieRentalEntities();
                }
                thumbnails = (from m in db.Movie
                              select new ThumbnailModel
                              {
                                  movieid = m.id,
                                  Title = m.Title,
                                  Description = m.Description,
                                  Image = m.Image,
                                  Link = "/Movie/Details/" + m.id

                              }).ToList();
            }
            catch (Exception ex)
            {

            }
            return thumbnails.OrderBy(mg => mg.Title);
        }
    }
}