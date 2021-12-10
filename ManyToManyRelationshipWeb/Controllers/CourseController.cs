using ManyToManyRelationshipWeb.Data;
using ManyToManyRelationshipWeb.Models;
using ManyToManyRelationshipWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ManyToManyRelationshipWeb.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public CourseController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var courses = _applicationDbContext.Courses
                                .OrderBy(x => x.CourseName)
                                .ToList();
            return View(courses);
        }

        [HttpGet]
        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                CourseViewModel model = new();

                return View(model);
            }
            else
            {
                var found = _applicationDbContext.Courses.Find(id.Value);

                if(found is null)
                {
                    return NotFound();
                }

                CourseViewModel model = new()
                {
                    Id = found.Id,
                    CourseName = found.CourseName,
                    CourseNumber = found.CourseNumber
                };

                return View(model);
            }


        }
        [HttpPost]
        public IActionResult Create(CourseViewModel model)
        {
            if (!ModelState.IsValid) { return View(model); }

            if (model.Id == 0)
            {
                //Create
                Course course = new()
                {
                    CourseName = model.CourseName,
                    CourseNumber = model.CourseNumber
                };

                _applicationDbContext.Courses.Add(course);
                _applicationDbContext.SaveChanges();
                return RedirectToAction("index", "course");
            }
            else
            {
                var courseFound = _applicationDbContext.Courses.Find(model.Id);

                if (courseFound is null)
                {
                    return NotFound();
                }

                courseFound.CourseName = model.CourseName;
                courseFound.CourseNumber = model.CourseNumber;

                _applicationDbContext.Courses.Update(courseFound);
                _applicationDbContext.SaveChanges();
                return RedirectToAction(nameof(Index), nameof(Course));

                //Edit
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            return View();
        }
        
    }
}
