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
    public class GeneralsController : ControllerBase
    {
        private readonly bcmm_ojaContext _context;

        public GeneralsController(bcmm_ojaContext context)
        {
            _context = context;
        }

        // GET: api/Generals
        [HttpGet]
        public IEnumerable<General> GetGeneral()
        {
            return _context.General;
        }

        // GET: api/Generals/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGeneral([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var general = await _context.General.FindAsync(id);

            if (general == null)
            {
                return NotFound();
            }

            return Ok(general);
        }

        // PUT: api/Generals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGeneral([FromRoute] int id, [FromBody] General general)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != general.Id)
            {
                return BadRequest();
            }

            _context.Entry(general).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GeneralExists(id))
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

        // POST: api/Generals
        [HttpPost]
        public async Task<IActionResult> PostGeneral([FromBody] General general)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.General.Add(general);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GeneralExists(general.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGeneral", new { id = general.Id }, general);
        }

        // DELETE: api/Generals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGeneral([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var general = await _context.General.FindAsync(id);
            if (general == null)
            {
                return NotFound();
            }

            _context.General.Remove(general);
            await _context.SaveChangesAsync();

            return Ok(general);
        }

        private bool GeneralExists(int id)
        {
            return _context.General.Any(e => e.Id == id);
        }
    }
}