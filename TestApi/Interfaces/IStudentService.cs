using TestApi.DTOs.students;

namespace TestApi.Interfaces
{
    public interface IStudentService
    {
        Task<List<StudentDto>> GetStudents();
        Task<StudentDto?> GetStudent(int id);
        Task<StudentDto> CreateStudent(StudentDto studentDto);
        Task<StudentDto?> UpdateStudent(int id, StudentDto studentDto);
        Task<StudentDto?> DeleteStudent(int id);
    }
}
