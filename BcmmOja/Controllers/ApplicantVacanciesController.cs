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
    public class ApplicantVacanciesController : ControllerBase
    {
        private readonly bcmm_ojaContext _context;

        public ApplicantVacanciesController(bcmm_ojaContext context)
        {
            _context = context;
        }

        // GET: api/ApplicantVacancies
        [HttpGet]
        public IEnumerable<ApplicantVacancy> GetApplicantVacancy()
        {
            return _context.ApplicantVacancy;
        }

        // GET: api/ApplicantVacancies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplicantVacancy([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicantVacancy = await _context.ApplicantVacancy.FindAsync(id);

            if (applicantVacancy == null)
            {
                return NotFound();
            }

            return Ok(applicantVacancy);
        }

        // PUT: api/ApplicantVacancies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicantVacancy([FromRoute] int id, [FromBody] ApplicantVacancy applicantVacancy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != applicantVacancy.Id)
            {
                return BadRequest();
            }

            _context.Entry(applicantVacancy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicantVacancyExists(id))
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

        // POST: api/ApplicantVacancies
        [HttpPost]
        public async Task<IActionResult> PostApplicantVacancy([FromBody] ApplicantVacancy applicantVacancy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ApplicantVacancy.Add(applicantVacancy);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplicantVacancy", new { id = applicantVacancy.Id }, applicantVacancy);
        }

        // DELETE: api/ApplicantVacancies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicantVacancy([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicantVacancy = await _context.ApplicantVacancy.FindAsync(id);
            if (applicantVacancy == null)
            {
                return NotFound();
            }

            _context.ApplicantVacancy.Remove(applicantVacancy);
            await _context.SaveChangesAsync();

            return Ok(applicantVacancy);
        }

        private bool ApplicantVacancyExists(int id)
        {
            return _context.ApplicantVacancy.Any(e => e.Id == id);
        }
    }
}