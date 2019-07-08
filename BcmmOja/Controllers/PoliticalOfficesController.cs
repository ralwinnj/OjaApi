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
    public class PoliticalOfficesController : ControllerBase
    {
        private readonly bcmm_ojaContext _context;

        public PoliticalOfficesController(bcmm_ojaContext context)
        {
            _context = context;
        }

        // GET: api/PoliticalOffices
        [HttpGet]
        public IEnumerable<PoliticalOffice> GetPoliticalOffice()
        {
            return _context.PoliticalOffice;
        }

        // GET: api/PoliticalOffices/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPoliticalOffice([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var politicalOffice = await _context.PoliticalOffice.FindAsync(id);

            if (politicalOffice == null)
            {
                return NotFound();
            }

            return Ok(politicalOffice);
        }

        // PUT: api/PoliticalOffices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPoliticalOffice([FromRoute] int id, [FromBody] PoliticalOffice politicalOffice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != politicalOffice.Id)
            {
                return BadRequest();
            }

            _context.Entry(politicalOffice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PoliticalOfficeExists(id))
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

        // POST: api/PoliticalOffices
        [HttpPost]
        public async Task<IActionResult> PostPoliticalOffice([FromBody] PoliticalOffice politicalOffice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PoliticalOffice.Add(politicalOffice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPoliticalOffice", new { id = politicalOffice.Id }, politicalOffice);
        }

        // DELETE: api/PoliticalOffices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePoliticalOffice([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var politicalOffice = await _context.PoliticalOffice.FindAsync(id);
            if (politicalOffice == null)
            {
                return NotFound();
            }

            _context.PoliticalOffice.Remove(politicalOffice);
            await _context.SaveChangesAsync();

            return Ok(politicalOffice);
        }

        private bool PoliticalOfficeExists(int id)
        {
            return _context.PoliticalOffice.Any(e => e.Id == id);
        }
    }
}