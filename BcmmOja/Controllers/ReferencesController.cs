using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BcmmOja.Models;
using VMD.RESTApiResponseWrapper.Core.Wrappers;
using VMD.RESTApiResponseWrapper.Core.Extensions;

namespace BcmmOja.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReferencesController : ControllerBase
    {
        private readonly bcmm_ojaContext _context;

        public ReferencesController(bcmm_ojaContext context)
        {
            _context = new bcmm_ojaContext();
        }

        // GET: api/References
        [HttpGet]
        public APIResponse GetReference()
        {
            try
            {
                return new APIResponse(200, "Success!", _context.Reference);
            }
            catch (SystemException ex)
            {
                return new APIResponse(500, "Server Error!", ex.InnerException);
            }
        }

        // GET: api/References/5
        [HttpGet("{id}")]
        public async Task<APIResponse> GetReference([FromRoute] int id)
        {
            var applicantId = id;
            try
            {

                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, "Validation Error", ModelStateExtension.AllErrors(ModelState));
                }

                var reference = await _context.Reference.Where(x => x.FkApplicantId == applicantId).ToListAsync();

                if (reference == null)
                {
                    return new APIResponse(404, $"Could not find reference(s) record with id of {applicantId}.");
                }

                return new APIResponse(200, $"Reference(s) records found.", reference);
            }
            catch (System.Exception ex)
            {
                return new APIResponse(500, "Server error!", ex.InnerException);
            }
        }

        // PUT: api/References/5
        [HttpPut("{id}")]
        public async Task<APIResponse> PutReference([FromRoute] int id, [FromBody] Reference reference)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, "Validation Error!", ModelStateExtension.AllErrors(ModelState));
                }
                if (!ReferenceExists(id))
                {
                    return new APIResponse(404, "Not found!");
                }
                if (id != reference.Id)
                {
                    return new APIResponse(409, $"Supplied id {id} does not match with the one in our records {reference.Id}.", ModelStateExtension.AllErrors(ModelState));
                }

                _context.Entry(reference).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return new APIResponse(200, $"Reference details updated successfully.", reference);
            }
            catch (System.Exception ex)
            {
                return new APIResponse(500, "Server Error!", ex.InnerException);
            }

        }

        // POST: api/References
        [HttpPost("{id}")]
        public async Task<APIResponse> PostReference([FromRoute] int id, [FromBody] Reference reference)
        {
            var applicantId = id;
            try
            {
                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, $"Validation Error!", ModelStateExtension.AllErrors(ModelState));
                }

                var aData = new Reference()
                {
                    Name = reference.Name,
                    Relationship = reference.Relationship,
                    TelNumber = reference.TelNumber,
                    CellNumber = reference.CellNumber,
                    Email = reference.Email,
                    CreatedAt = DateTime.Now,
                    FkApplicantId = applicantId
                };

                await _context.Reference.AddAsync(aData);
                await _context.SaveChangesAsync();
                return new APIResponse(200, $"Success!", aData);

            }
            catch (SystemException ex)
            {
                return new APIResponse(500, "Server Error", ex.InnerException);
            }
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