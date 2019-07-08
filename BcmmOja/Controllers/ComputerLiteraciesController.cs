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
    public class ComputerLiteraciesController : ControllerBase
    {
        private readonly bcmm_ojaContext _context;

        public ComputerLiteraciesController(bcmm_ojaContext context)
        {
            _context = context;
        }

        // GET: api/ComputerLiteracies
        [HttpGet]
        public IEnumerable<ComputerLiteracy> GetComputerLiteracy()
        {
            return _context.ComputerLiteracy;
        }

        // GET: api/ComputerLiteracies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComputerLiteracy([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var computerLiteracy = await _context.ComputerLiteracy.FindAsync(id);

            if (computerLiteracy == null)
            {
                return NotFound();
            }

            return Ok(computerLiteracy);
        }

        // PUT: api/ComputerLiteracies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComputerLiteracy([FromRoute] int id, [FromBody] ComputerLiteracy computerLiteracy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != computerLiteracy.Id)
            {
                return BadRequest();
            }

            _context.Entry(computerLiteracy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComputerLiteracyExists(id))
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

        // POST: api/ComputerLiteracies
        [HttpPost]
        public async Task<IActionResult> PostComputerLiteracy([FromBody] ComputerLiteracy computerLiteracy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ComputerLiteracy.Add(computerLiteracy);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComputerLiteracy", new { id = computerLiteracy.Id }, computerLiteracy);
        }

        // DELETE: api/ComputerLiteracies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComputerLiteracy([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var computerLiteracy = await _context.ComputerLiteracy.FindAsync(id);
            if (computerLiteracy == null)
            {
                return NotFound();
            }

            _context.ComputerLiteracy.Remove(computerLiteracy);
            await _context.SaveChangesAsync();

            return Ok(computerLiteracy);
        }

        private bool ComputerLiteracyExists(int id)
        {
            return _context.ComputerLiteracy.Any(e => e.Id == id);
        }
    }
}