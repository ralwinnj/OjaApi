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
    public class ProfessionalMembershipsController : ControllerBase
    {
        private readonly bcmm_ojaContext _context;

        public ProfessionalMembershipsController(bcmm_ojaContext context)
        {
            _context = context;
        }

        // GET: api/ProfessionalMemberships
        [HttpGet]
        public IEnumerable<ProfessionalMembership> GetProfessionalMembership()
        {
            return _context.ProfessionalMembership;
        }

        // GET: api/ProfessionalMemberships/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfessionalMembership([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var professionalMembership = await _context.ProfessionalMembership.FindAsync(id);

            if (professionalMembership == null)
            {
                return NotFound();
            }

            return Ok(professionalMembership);
        }

        // PUT: api/ProfessionalMemberships/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfessionalMembership([FromRoute] int id, [FromBody] ProfessionalMembership professionalMembership)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != professionalMembership.Id)
            {
                return BadRequest();
            }

            _context.Entry(professionalMembership).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfessionalMembershipExists(id))
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

        // POST: api/ProfessionalMemberships
        [HttpPost]
        public async Task<IActionResult> PostProfessionalMembership([FromBody] ProfessionalMembership professionalMembership)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ProfessionalMembership.Add(professionalMembership);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfessionalMembership", new { id = professionalMembership.Id }, professionalMembership);
        }

        // DELETE: api/ProfessionalMemberships/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfessionalMembership([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var professionalMembership = await _context.ProfessionalMembership.FindAsync(id);
            if (professionalMembership == null)
            {
                return NotFound();
            }

            _context.ProfessionalMembership.Remove(professionalMembership);
            await _context.SaveChangesAsync();

            return Ok(professionalMembership);
        }

        private bool ProfessionalMembershipExists(int id)
        {
            return _context.ProfessionalMembership.Any(e => e.Id == id);
        }
    }
}