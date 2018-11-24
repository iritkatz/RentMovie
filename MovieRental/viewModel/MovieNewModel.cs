using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieRental.viewModel
{
    public class MovieNewModel
    {
        public IEnumerable<MovieRental.Genre> Genres { get; set; }
        public Movie movie { get; set; }
    }
}