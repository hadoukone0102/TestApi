using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApi.Models;
using TestApi.Services;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        // variable
        private readonly StudentService _studentService;
        // Constructor
        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }
        // Get all students
        [HttpGet]
        public async Task<ActionResult<List<StudentTest>>> GetStudents()
        {
            return await _studentService.GetStudents();
        }
        // Get student by id
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<StudentTest>> GetStudent(int id)
        {
            var student = await _studentService.GetStudent(id);
            if (student == null)
            {
                return NotFound();
            }
            return student;
        }
        // Create student
        [HttpPost]
        public async Task<ActionResult<StudentTest>> CreateStudent(StudentTest student)
        {
            var newStudent = await _studentService.CreateStudent(student);
            return CreatedAtAction(nameof(GetStudent), new { id = newStudent.Id }, newStudent);
        }
        // Update student
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<StudentTest>> UpdateStudent(int id, StudentTest student)
        {
            var updatedStudent = await _studentService.UpdateStudent(id, student);
            if (updatedStudent == null)
            {
                return NotFound();
            }
            return updatedStudent;
        }
        // Delete student
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<StudentTest>> DeleteStudent(int id)
        {
            var student = await _studentService.DeleteStudent(id);
            if (student == null)
            {
                return NotFound();
            }
            return student;
        }
    }
}
