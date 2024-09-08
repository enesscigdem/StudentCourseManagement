using Microsoft.AspNetCore.Mvc;
using StudentCourseManagement.Application.Services;
using StudentCourseManagement.Domain.Entities;
using System.Threading.Tasks;
using StudentCourseManagement.Application.Interfaces;

namespace StudentCourseManagement.Presentation.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return View(students);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                await _studentService.AddStudentAsync(student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _studentService.UpdateStudentAsync(student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _studentService.DeleteStudentAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
