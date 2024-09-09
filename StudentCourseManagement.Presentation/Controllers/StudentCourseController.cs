using Microsoft.AspNetCore.Mvc;
using StudentCourseManagement.Application.Services;
using StudentCourseManagement.Domain.Entities;
using StudentCourseManagement.Application.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using StudentCourseManagement.Application.ViewModels;

namespace StudentCourseManagement.Presentation.Controllers
{
    [Authorize]
    public class StudentCourseController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;

        public StudentCourseController(IStudentService studentService, ICourseService courseService)
        {
            _studentService = studentService;
            _courseService = courseService;
        }

        public async Task<IActionResult> AssignCourse(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            // Tüm dersleri almak için uygun bir servis metodunu kullanın
            var availableCourses = await _courseService.GetAllCoursesAsync();

            // Modeli oluşturun
            var model = new AssignCourseViewModel
            {
                Student = student,
                AvailableCourses = availableCourses, // Burada AvailableCourses null olmamalı
                SelectedCourseIds = new List<int>() // Boş bir liste başlatabilirsiniz
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignCourse(AssignCourseViewModel model)
        {
            var getStudent = await _studentService.GetStudentByIdAsync(model.Student.StudentId);
            var availableCourses = await _courseService.GetAllCoursesAsync();
            if (model.AvailableCourses == null)
                model.AvailableCourses = availableCourses;
            model.Student = getStudent;

            // Öğrenciyi ve dersleri al
            var student = await _studentService.GetStudentByIdAsync(model.Student.StudentId);
            if (student == null)
            {
                return NotFound();
            }

            // Dersleri atama işlemi
            await _studentService.AssignCoursesToStudentAsync(model.Student.StudentId, model.SelectedCourseIds);

            return RedirectToAction("Details", "Student", new { id = model.Student.StudentId });
        }
    }
}