using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManyToManyRelationshipWeb.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        [Required, Display(Name ="Course Name")]
        public string CourseName { get; set; }
        [Required, Display(Name ="Course Number")]
        public int CourseNumber { get; set; }
    }
}
