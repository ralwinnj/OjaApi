using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BcmmOja.Models;
using VMD.RESTApiResponseWrapper.Core.Wrappers;
using VMD.RESTApiResponseWrapper.Core.Extensions;

namespace BcmmOja.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacanciesController : ControllerBase
    {
        private readonly bcmm_ojaContext _context;

        public VacanciesController(bcmm_ojaContext context)
        {
            _context = new bcmm_ojaContext();
        }

        // GET: api/Vacancies
        [HttpGet]
        public APIResponse GetVacancy()
        {
            try
            {
                return new APIResponse(200, "Success!", _context.Vacancy);
            }
            catch (SystemException ex)
            {
                return new APIResponse(500, "Server Error!", ex.InnerException);
            }
        }

        // GET: api/Vacancies/5
        [HttpGet("{id}")]
        public async Task<APIResponse> GetVacancy([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, "Validation error!", ModelStateExtension.AllErrors(ModelState));
                }

                var vacancy = await _context.Vacancy.FindAsync(id);

                if (vacancy == null)
                {
                    return new APIResponse(404, "Record not found");
                }

                return new APIResponse(200, "Success!", vacancy);
            } 
            catch (System.Exception ex)
            {
                return new APIResponse(500, "Server error", ex.InnerException);
            }
            
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