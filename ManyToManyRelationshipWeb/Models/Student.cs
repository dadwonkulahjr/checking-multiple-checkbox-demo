using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManyToManyRelationshipWeb.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public List<StudentCourse> StudentCourses { get; set; }
    }
}
