using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApi.Data;
using TestApi.DTOs.students;
using TestApi.Interfaces;
using TestApi.Models;

namespace TestApi.Services
{
    public class StudentService : IStudentService
    {
        // Fields
        private readonly AppDbContext _context;
        // Constructor
        public StudentService(AppDbContext context)
        {
            _context = context;
        }

        // Methods get all students liste
        public async Task<List<StudentDto>> GetStudents()
        {
            var students = await _context.StudentTests.ToListAsync();
            return students.Select(student => new StudentDto
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Class = student.Class,
                Field = student.Field,
                Matricule = student.Matricule,
                Contact = student.Contact,
                Email = student.Email
            }).ToList();
        }

        // Method get student by id
        public async Task<StudentDto?> GetStudent(int id)
        {
            var student = await _context.StudentTests.FindAsync(id);
            return student == null
                ? null
                : new StudentDto
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Class = student.Class,
                    Field = student.Field,
                    Matricule = student.Matricule,
                    Contact = student.Contact,
                    Email = student.Email
                };
        }
        // Method create student
        public async Task<StudentDto> CreateStudent(StudentDto studentDto)
        {
            var student = new StudentTest
            {
                FirstName = studentDto.FirstName,
                LastName = studentDto.LastName,
                Class = studentDto.Class,
                Field = studentDto.Field,
                Matricule = studentDto.Matricule,
                Contact = studentDto.Contact,
                Email = studentDto.Email
            };

            _context.StudentTests.Add(student);
            await _context.SaveChangesAsync();
            return studentDto;
        }
        // Method to update a student
        public async Task<StudentDto?> UpdateStudent(int id, StudentDto studentDto)
        {
            // Vérifier si l'étudiant existe
            var existingStudent = await _context.StudentTests.FindAsync(id);

            if (existingStudent == null)
            {
                return null;
            }

            // Mise à jour des propriétés
            existingStudent.FirstName = studentDto.FirstName;
            existingStudent.LastName = studentDto.LastName;
            existingStudent.Class = studentDto.Class;
            existingStudent.Field = studentDto.Field;
            existingStudent.Matricule = studentDto.Matricule;
            existingStudent.Contact = studentDto.Contact;
            existingStudent.Email = studentDto.Email;

            await _context.SaveChangesAsync();
            return studentDto;
        }
        // Method delete student
        public async Task<StudentDto?> DeleteStudent(int id)
        {
            //Vérifier si l'étudiant existe
            var existingStudent = await _context.StudentTests.FindAsync(id);
            if (existingStudent == null)
            {
                return null;
            }

            //Supprimer l'étudiant
            _context.StudentTests.Remove(existingStudent);
            await _context.SaveChangesAsync();
            return new StudentDto
            {
                FirstName = existingStudent.FirstName,
                LastName = existingStudent.LastName,
                Class = existingStudent.Class,
                Field = existingStudent.Field,
                Matricule = existingStudent.Matricule,
                Contact = existingStudent.Contact,
                Email = existingStudent.Email
            };
        }

    }
}
