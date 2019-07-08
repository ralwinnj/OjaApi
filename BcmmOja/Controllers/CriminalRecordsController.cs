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
    public class CriminalRecordsController : ControllerBase
    {
        private readonly bcmm_ojaContext _context;

        public CriminalRecordsController(bcmm_ojaContext context)
        {
            _context = context;
        }

        // GET: api/CriminalRecords
        [HttpGet]
        public IEnumerable<CriminalRecord> GetCriminalRecord()
        {
            return _context.CriminalRecord;
        }

        // GET: api/CriminalRecords/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCriminalRecord([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var criminalRecord = await _context.CriminalRecord.FindAsync(id);

            if (criminalRecord == null)
            {
                return NotFound();
            }

            return Ok(criminalRecord);
        }

        // PUT: api/CriminalRecords/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCriminalRecord([FromRoute] int id, [FromBody] CriminalRecord criminalRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != criminalRecord.Id)
            {
                return BadRequest();
            }

            _context.Entry(criminalRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CriminalRecordExists(id))
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

        // POST: api/CriminalRecords
        [HttpPost]
        public async Task<IActionResult> PostCriminalRecord([FromBody] CriminalRecord criminalRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CriminalRecord.Add(criminalRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCriminalRecord", new { id = criminalRecord.Id }, criminalRecord);
        }

        // DELETE: api/CriminalRecords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCriminalRecord([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var criminalRecord = await _context.CriminalRecord.FindAsync(id);
            if (criminalRecord == null)
            {
                return NotFound();
            }

            _context.CriminalRecord.Remove(criminalRecord);
            await _context.SaveChangesAsync();

            return Ok(criminalRecord);
        }

        private bool CriminalRecordExists(int id)
        {
            return _context.CriminalRecord.Any(e => e.Id == id);
        }
    }
}