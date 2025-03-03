using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApi.Data;
using TestApi.Models;
using TestApi.Services;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursController : ControllerBase
    {
        //Variables
        private readonly CoursService _courServices;
        //Constructor
        public CoursController(CoursService courServices)
        {
            _courServices = courServices;
        }
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<List<Cours>>> GetCours()
        {
            return await _courServices.GetCourses();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cours>> GetCoursById(int id)
        { 
            var cours = await _courServices.GetCours(id);
            if(cours == null)
            {
                return NotFound();
            }
            return cours;
        }
        // POST api/values
        [HttpPost]
        public async Task<ActionResult<Cours>> CreateCours(Cours cours)
        {
            var newCours = await _courServices.CreateCours(cours);
            return CreatedAtAction(nameof(GetCoursById), new { id = newCours.Id }, newCours);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Cours>> UpdateCours(int id, Cours cours)
        {
            var updatedCours = await _courServices.UpdateCours(id, cours);
            if (updatedCours == null)
            {
                return NotFound();
            }
            return updatedCours;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cours>> DeleteCours(int id)
        {
            var cours = await _courServices.DeleteCours(id);
            if (cours == null)
            {
                return NotFound();
            }
            return cours;
        }

    }
}
