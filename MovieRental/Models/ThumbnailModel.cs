using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieRental.Models
{
    public class ThumbnailModel
    {
        internal object thumbnils;

        public int movieid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }      
        public string Image { get; set; }
        public string Link { get; set; }

    }
}