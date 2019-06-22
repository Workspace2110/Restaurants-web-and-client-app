using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cloud_homework4.Models
{
    public class RestaurantGenreModel
    {
        public List<Restaurant> Restaurants;
        public SelectList Genres;
        public string RestaurantGenres { get; set; }
        public string Str_search { get; set; }
    }
}
