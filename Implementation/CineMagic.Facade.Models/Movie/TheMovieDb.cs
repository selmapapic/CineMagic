using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CineMagic.Facade.Models.Movie
{
    public class TheMovieDb
    {

        public string Poster_path { get; set; }
        public bool Adult { get; set; }
        public string Overview { get; set; }
        public string Release_date { get; set; }
        public string Original_title { get; set; }
        public List<object> Genre_ids { get; set; }
        public int Id { get; set; }
        public string Media_type { get; set; }
        public string Original_language { get; set; }
        public string Title { get; set; }
        public string Backdrop_path { get; set; }
        public double Popularity { get; set; }
        public int Vote_count { get; set; }
        public bool Video { get; set; }
        public double Vote_average { get; set; }
        public string First_air_date { get; set; }
        public List<string> Origin_country { get; set; }
        public string Name { get; set; }
        public string Original_name { get; set; }
    }
}
