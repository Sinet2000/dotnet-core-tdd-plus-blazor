using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorExperience.Core.Models.Bases;

namespace BlazorExperience.Core.Models
{
    public class Book : ModelBase
    {
        [Required]
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
    }
}
