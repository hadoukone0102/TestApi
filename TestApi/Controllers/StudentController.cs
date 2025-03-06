using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApi.DTOs.students;
using TestApi.Helpers;
using TestApi.Interfaces;
using TestApi.Models;
using TestApi.Services;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        // variable
        private readonly IStudentService _studentService;
        // Constructor
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        // Get all students
        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _studentService.GetStudents();
            return Ok(new ApiResponse<List<StudentDto>>(true,"Liste des étudiant récupérer avec succès",students));
        }
        // Get student by id
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await _studentService.GetStudent(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(new ApiResponse<StudentDto>(true,"Etudiant récupérer avec succès",student));
        }
        // Create student
        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] StudentDto studentDto)
        {
            var newStudent = await _studentService.CreateStudent(studentDto);
            return CreatedAtAction(nameof(GetStudent), new { id = studentDto.FirstName },
                new ApiResponse<StudentDto>(true, "Etudiant créer avec succès", newStudent)
            );
        }
        // Update student
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateStudent(int id,[FromBody] StudentDto studentDto)
        {
            var updatedStudent = await _studentService.UpdateStudent(id, studentDto);
            if (updatedStudent == null)
            {
                return NotFound();
            }
            return Ok(new ApiResponse<StudentDto>(true,"Etudiant mis à jour avec succès.",updatedStudent));
        }
        // Delete student
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _studentService.DeleteStudent(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(new ApiResponse<StudentDto>(true,"",student));
        }
    }
}
