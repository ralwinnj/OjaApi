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
    public class ExperiencesController : ControllerBase
    {
        private readonly bcmm_ojaContext _context;

        public ExperiencesController(bcmm_ojaContext context)
        {
            _context = context;
        }

        // GET: api/Experiences
        [HttpGet]
        public IEnumerable<Experience> GetExperience()
        {
            return _context.Experience;
        }

        // GET: api/Experiences/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExperience([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var experience = await _context.Experience.FindAsync(id);

            if (experience == null)
            {
                return NotFound();
            }

            return Ok(experience);
        }

        // PUT: api/Experiences/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExperience([FromRoute] int id, [FromBody] Experience experience)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != experience.Id)
            {
                return BadRequest();
            }

            _context.Entry(experience).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExperienceExists(id))
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

        // POST: api/Experiences
        [HttpPost]
        public async Task<IActionResult> PostExperience([FromBody] Experience experience)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Experience.Add(experience);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExperience", new { id = experience.Id }, experience);
        }

        // DELETE: api/Experiences/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExperience([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var experience = await _context.Experience.FindAsync(id);
            if (experience == null)
            {
                return NotFound();
            }

            _context.Experience.Remove(experience);
            await _context.SaveChangesAsync();

            return Ok(experience);
        }

        private bool ExperienceExists(int id)
        {
            return _context.Experience.Any(e => e.Id == id);
        }
    }
}