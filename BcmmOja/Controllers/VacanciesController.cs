using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BcmmOja.Models;

namespace BcmmOja.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacanciesController : ControllerBase
    {
        private readonly bcmm_ojaContext _context;

        public VacanciesController(bcmm_ojaContext context)
        {
            _context = context;
        }

        // GET: api/Vacancies
        [HttpGet]
        public IEnumerable<Vacancy> GetVacancy()
        {
            return _context.Vacancy;
        }

        // GET: api/Vacancies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVacancy([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vacancy = await _context.Vacancy.FindAsync(id);

            if (vacancy == null)
            {
                return NotFound();
            }

            return Ok(vacancy);
        }

        // PUT: api/Vacancies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVacancy([FromRoute] int id, [FromBody] Vacancy vacancy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vacancy.Id)
            {
                return BadRequest();
            }

            _context.Entry(vacancy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VacancyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Vacancies
        [HttpPost]
        public async Task<IActionResult> PostVacancy([FromBody] Vacancy vacancy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Vacancy.Add(vacancy);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVacancy", new { id = vacancy.Id }, vacancy);
        }

        // DELETE: api/Vacancies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVacancy([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vacancy = await _context.Vacancy.FindAsync(id);
            if (vacancy == null)
            {
                return NotFound();
            }

            _context.Vacancy.Remove(vacancy);
            await _context.SaveChangesAsync();

            return Ok(vacancy);
        }

        private bool VacancyExists(int id)
        {
            return _context.Vacancy.Any(e => e.Id == id);
        }
    }
}