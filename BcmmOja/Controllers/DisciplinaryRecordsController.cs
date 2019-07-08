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
    public class DisciplinaryRecordsController : ControllerBase
    {
        private readonly bcmm_ojaContext _context;

        public DisciplinaryRecordsController(bcmm_ojaContext context)
        {
            _context = context;
        }

        // GET: api/DisciplinaryRecords
        [HttpGet]
        public IEnumerable<DisciplinaryRecord> GetDisciplinaryRecord()
        {
            return _context.DisciplinaryRecord;
        }

        // GET: api/DisciplinaryRecords/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDisciplinaryRecord([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var disciplinaryRecord = await _context.DisciplinaryRecord.FindAsync(id);

            if (disciplinaryRecord == null)
            {
                return NotFound();
            }

            return Ok(disciplinaryRecord);
        }

        // PUT: api/DisciplinaryRecords/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDisciplinaryRecord([FromRoute] int id, [FromBody] DisciplinaryRecord disciplinaryRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != disciplinaryRecord.Id)
            {
                return BadRequest();
            }

            _context.Entry(disciplinaryRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DisciplinaryRecordExists(id))
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

        // POST: api/DisciplinaryRecords
        [HttpPost]
        public async Task<IActionResult> PostDisciplinaryRecord([FromBody] DisciplinaryRecord disciplinaryRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.DisciplinaryRecord.Add(disciplinaryRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDisciplinaryRecord", new { id = disciplinaryRecord.Id }, disciplinaryRecord);
        }

        // DELETE: api/DisciplinaryRecords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDisciplinaryRecord([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var disciplinaryRecord = await _context.DisciplinaryRecord.FindAsync(id);
            if (disciplinaryRecord == null)
            {
                return NotFound();
            }

            _context.DisciplinaryRecord.Remove(disciplinaryRecord);
            await _context.SaveChangesAsync();

            return Ok(disciplinaryRecord);
        }

        private bool DisciplinaryRecordExists(int id)
        {
            return _context.DisciplinaryRecord.Any(e => e.Id == id);
        }
    }
}