using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApi.Data;
using TestApi.Models;

namespace TestApi.Services
{
    public class StudentService
    {
        // Fields
        private readonly AppDbContext _context;
        // Constructor
        public StudentService(AppDbContext context)
        {
            _context = context;
        }

        // Methods get all students liste
        public async Task<List<StudentTest>> GetStudents()
        {
            return await _context.StudentTests.ToListAsync();
        }
        // Method get student by id
        public async Task<StudentTest?> GetStudent(int id)
        {
            return await _context.StudentTests.FindAsync(id);
        }
        // Method create student
        public async Task<StudentTest> CreateStudent(StudentTest student)
        {
            _context.StudentTests.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }
        // Method to update a student
        public async Task<StudentTest?> UpdateStudent(int id, StudentTest student)
        {
            // Vérifier si l'étudiant existe
            var existingStudent = await _context.StudentTests.FindAsync(id);

            if (existingStudent == null)
            {
                return null;
            }

            // Mise à jour des propriétés
            existingStudent.FirstName = student.FirstName;
            existingStudent.LastName = student.LastName;
            existingStudent.Class = student.Class;
            existingStudent.Field = student.Field;
            existingStudent.Matricule = student.Matricule;
            existingStudent.Contact = student.Contact;
            existingStudent.Email = student.Email;

            await _context.SaveChangesAsync();
            return existingStudent;
        }
        // Method delete student
        public async Task<StudentTest?> DeleteStudent(int id)
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
            return existingStudent;
        }

    }
}
