using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManyToManyRelationshipWeb.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string CourseName { get; set; }
        [Required]
        public int CourseNumber { get; set; }

        public List<StudentCourse> StudentCourses { get; set; }
    }
}
