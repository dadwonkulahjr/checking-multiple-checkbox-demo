using ManyToManyRelationshipWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManyToManyRelationshipWeb.ViewModels
{
    public class StudentViewModel
    {
        public List<Student> ListOfStudents { get; set; }
        public List<StudentCourse> ListOfStudentCourse { get; set; }
    }
}
