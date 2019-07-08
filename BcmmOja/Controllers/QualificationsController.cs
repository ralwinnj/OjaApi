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
    public class QualificationsController : ControllerBase
    {
        private readonly bcmm_ojaContext _context;

        public QualificationsController(bcmm_ojaContext context)
        {
            _context = context;
        }

        // GET: api/Qualifications
        [HttpGet]
        public IEnumerable<Qualification> GetQualification()
        {
            return _context.Qualification;
        }

        // GET: api/Qualifications/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQualification([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var qualification = await _context.Qualification.FindAsync(id);

            if (qualification == null)
            {
                return NotFound();
            }

            return Ok(qualification);
        }

        // PUT: api/Qualifications/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQualification([FromRoute] int id, [FromBody] Qualification qualification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != qualification.Id)
            {
                return BadRequest();
            }

            _context.Entry(qualification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QualificationExists(id))
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

        // POST: api/Qualifications
        [HttpPost]
        public async Task<IActionResult> PostQualification([FromBody] Qualification qualification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Qualification.Add(qualification);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQualification", new { id = qualification.Id }, qualification);
        }

        // DELETE: api/Qualifications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQualification([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var qualification = await _context.Qualification.FindAsync(id);
            if (qualification == null)
            {
                return NotFound();
            }

            _context.Qualification.Remove(qualification);
            await _context.SaveChangesAsync();

            return Ok(qualification);
        }

        private bool QualificationExists(int id)
        {
            return _context.Qualification.Any(e => e.Id == id);
        }
    }
}