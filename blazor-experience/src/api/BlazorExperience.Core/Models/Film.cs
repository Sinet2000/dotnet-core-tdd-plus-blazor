using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorExperience.Core.Models.Bases;

namespace BlazorExperience.Core.Models
{
    public class Film : ModelBase
    {
        public string Title { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public float IMDbRating { get; set; }
    }
}
