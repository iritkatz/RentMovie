using MovieRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieRental.viewModel
{
    public class thumbnailboxViewModel
    {
        public IEnumerable <ThumbnailModel> Thumbnail{ get; set; }

    }
}