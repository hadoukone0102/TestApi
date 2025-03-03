using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApi.Data;
using TestApi.Models;

namespace TestApi.Services
{
    public class CoursService
    {
        // variable
        private readonly AppDbContext _coursService;
        // Constructor
        public CoursService(AppDbContext coursService)
        {
            _coursService = coursService;
        }

        // Methode Get all cours
        public async Task<List<Cours>> GetCourses()
        {
            return await _coursService.Courses.ToListAsync();
        }

        // Methode Get cours by id
        public async Task<Cours?> GetCours(int id)
        {
            return await _coursService.Courses.FindAsync(id);
        }

        // Methode Create cours
        public async Task<Cours> CreateCours(Cours cours)
        {
            _coursService.Courses.Add(cours);
            await _coursService.SaveChangesAsync();
            return cours;
        }

        // Methode Update cours

        public async Task<Cours?> UpdateCours(int id, Cours cours)
        {
            // Vérifier si le cours existe
            var existingCours = await _coursService.Courses.FindAsync(id);
            if (existingCours == null)
            {
                return null;
            }
            // Mise à jour des propriétés
            existingCours.CourName = cours.CourName;
            existingCours.CourCode = cours.CourCode;
            existingCours.CourDescription = cours.CourDescription;
            existingCours.CoursTeacher = cours.CoursTeacher;
            await _coursService.SaveChangesAsync();
            return existingCours;
        }

        // Methode Delete cours
        public async Task<Cours?> DeleteCours(int id)
        {
            // Vérifier si le cours existe
            var existingCours = await _coursService.Courses.FindAsync(id);
            if (existingCours == null)
            {
                return null;
            }
            _coursService.Courses.Remove(existingCours);
            await _coursService.SaveChangesAsync();
            return existingCours;
        }
    }
}
