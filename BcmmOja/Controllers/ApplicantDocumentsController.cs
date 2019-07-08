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
    public class ApplicantDocumentsController : ControllerBase
    {
        private readonly bcmm_ojaContext _context;

        public ApplicantDocumentsController()
        {
            _context = new bcmm_ojaContext();
        }

        // GET: api/ApplicantDocuments
        [HttpGet]
        public IEnumerable<ApplicantDocument> GetApplicantDocument()
        {
            return _context.ApplicantDocument;
        }

        // GET: api/ApplicantDocuments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplicantDocument([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicantDocument = await _context.ApplicantDocument.FindAsync(id);

            if (applicantDocument == null)
            {
                return NotFound();
            }

            return Ok(applicantDocument);
        }

        // PUT: api/ApplicantDocuments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicantDocument([FromRoute] int id, [FromBody] ApplicantDocument applicantDocument)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != applicantDocument.Id)
            {
                return BadRequest();
            }

            _context.Entry(applicantDocument).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicantDocumentExists(id))
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

        // POST: api/ApplicantDocuments
        [HttpPost]
        public async Task<IActionResult> PostApplicantDocument([FromBody] ApplicantDocument applicantDocument)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ApplicantDocument.Add(applicantDocument);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplicantDocument", new { id = applicantDocument.Id }, applicantDocument);
        }

        // DELETE: api/ApplicantDocuments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicantDocument([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicantDocument = await _context.ApplicantDocument.FindAsync(id);
            if (applicantDocument == null)
            {
                return NotFound();
            }

            _context.ApplicantDocument.Remove(applicantDocument);
            await _context.SaveChangesAsync();

            return Ok(applicantDocument);
        }

        private bool ApplicantDocumentExists(int id)
        {
            return _context.ApplicantDocument.Any(e => e.Id == id);
        }
    }
}