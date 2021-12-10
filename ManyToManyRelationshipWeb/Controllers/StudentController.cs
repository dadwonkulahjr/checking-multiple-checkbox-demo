using ManyToManyRelationshipWeb.Data;
using ManyToManyRelationshipWeb.Models;
using ManyToManyRelationshipWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManyToManyRelationshipWeb.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public StudentController(ApplicationDbContext applicationDbCotnext)
        {
            _applicationDbContext = applicationDbCotnext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            StudentViewModel model = new();
            model.ListOfStudents = _applicationDbContext.Students
                                              .Include(x => x.StudentCourses)
                                              .ThenInclude(x => x.Course)
                                              .ToList();

            foreach (var item in model.ListOfStudents)
            {
                model.ListOfStudentCourse = item.StudentCourses;
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                var retrivedAllCourses = _applicationDbContext.Courses.ToList();
                StudentCourseViewModel model = new();

                model.AvailableCourses = retrivedAllCourses.Select(c => new CustomCheckBoxItem()
                {
                    Id = c.Id,
                    Title = c.CourseName,
                    IsChecked = false
                }).ToList();

                return View(model);
            }
            else
            {

                StudentCourseViewModel model = new();
                var student = _applicationDbContext.Students
                                        .Include(x => x.StudentCourses)
                                        .ThenInclude(x => x.Course)
                                        .AsNoTracking()
                                        .FirstOrDefault(x => x.Id == id.Value);

                var allCourses = _applicationDbContext.Courses.Select(x => new CustomCheckBoxItem()
                {
                    Id = x.Id,
                    Title = x.CourseName,
                    IsChecked = x.StudentCourses.Any(x => x.StudentId == student.Id)
                }).ToList();

                model.FirstName = student.FirstName;
                model.LastName = student.LastName;
                model.AvailableCourses = allCourses;

                return View(model);
            }


        }

        [HttpPost]
        public IActionResult Create(StudentCourseViewModel model, Student student, StudentCourse studentCourse)
        {
            if (!ModelState.IsValid) { return View(model); }


            if (model.Id == 0)
            {
                List<StudentCourse> studentCourses = new();
                student.FirstName = model.FirstName;
                student.LastName = model.LastName;
                _applicationDbContext.Students.Add(student);
                _applicationDbContext.SaveChanges();
                int studentId = student.Id;

                foreach (var course in model.AvailableCourses)
                {
                    if (course.IsChecked)
                    {
                        studentCourses.Add(new StudentCourse() { StudentId = studentId, CourseId = course.Id });
                    }
                }

                foreach (var stc in studentCourses)
                {
                    _applicationDbContext.StudentCourses.Add(stc);
                }

                _applicationDbContext.SaveChanges();
                return RedirectToAction(nameof(Index), nameof(Student));
            }
            else
            {
                List<StudentCourse> studentCourses = new();
                student.FirstName = model.FirstName;
                student.LastName = model.LastName;
                _applicationDbContext.Students.Update(student);
                _applicationDbContext.SaveChanges();
                int studentId = student.Id;

                foreach (var course in model.AvailableCourses)
                {
                    if (course.IsChecked)
                    {
                        studentCourses.Add(new StudentCourse() { StudentId = studentId, CourseId = course.Id });
                    }
                }

                var dataTableSet = _applicationDbContext.StudentCourses.Where(s => s.StudentId == studentId).ToList();

                if (dataTableSet != null)
                {
                    var resultList = dataTableSet.Except(studentCourses).ToList();
                    foreach (var list in resultList)
                    {
                        _applicationDbContext.StudentCourses.Remove(list);
                        _applicationDbContext.SaveChanges();
                    }
                }

                var retriveCourseId = _applicationDbContext.StudentCourses.Where(s => s.StudentId == studentId).ToList();

                foreach (var item in studentCourses)
                {
                    if(!retriveCourseId.Contains(item))
                    {
                        _applicationDbContext.StudentCourses.Add(item);
                        _applicationDbContext.SaveChanges();
                    }
                }



                return RedirectToAction(nameof(Index), nameof(Student));
            }


           

        }
    }
}
