using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorExperience.Shared.ViewModels.Film
{
    public class FilmViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public float IMDbRating { get; set; }
    }
}
