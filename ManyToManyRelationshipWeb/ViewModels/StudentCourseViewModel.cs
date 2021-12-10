using System.Collections.Generic;

namespace ManyToManyRelationshipWeb.ViewModels
{
    public class StudentCourseViewModel
    {

        public int Id { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<CustomCheckBoxItem> AvailableCourses { get; set; }

    }
}
