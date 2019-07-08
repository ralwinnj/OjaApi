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
    public class ReferencesController : ControllerBase
    {
        private readonly bcmm_ojaContext _context;

        public ReferencesController(bcmm_ojaContext context)
        {
            _context = context;
        }

        // GET: api/References
        [HttpGet]
        public IEnumerable<Reference> GetReference()
        {
            return _context.Reference;
        }

        // GET: api/References/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReference([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reference = await _context.Reference.FindAsync(id);

            if (reference == null)
            {
                return NotFound();
            }

            return Ok(reference);
        }

        // PUT: api/References/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReference([FromRoute] int id, [FromBody] Reference reference)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reference.Id)
            {
                return BadRequest();
            }

            _context.Entry(reference).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReferenceExists(id))
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

        // POST: api/References
        [HttpPost]
        public async Task<IActionResult> PostReference([FromBody] Reference reference)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Reference.Add(reference);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReference", new { id = reference.Id }, reference);
        }

        // DELETE: api/References/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReference([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reference = await _context.Reference.FindAsync(id);
            if (reference == null)
            {
                return NotFound();
            }

            _context.Reference.Remove(reference);
            await _context.SaveChangesAsync();

            return Ok(reference);
        }

        private bool ReferenceExists(int id)
        {
            return _context.Reference.Any(e => e.Id == id);
        }
    }
}