using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BcmmOja.Models;
using VMD.RESTApiResponseWrapper.Core.Wrappers;
using VMD.RESTApiResponseWrapper.Core.Extensions;

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
        public APIResponse GetApplicantDocument()
        {
            try
            {
                return new APIResponse(200, $"Success", _context.ApplicantDocument);
            }
            catch (SystemException ex)
            {
                return new APIResponse(500, $"Server error", ex.Message);
            }
            
        }

        // GET: api/ApplicantDocuments/5
        [HttpGet("{id}")]
        public async Task<APIResponse> GetApplicantDocument([FromRoute] int id)
        {
            var applicantId = id;
            try
            {
                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, $"Validation error.", ModelStateExtension.AllErrors(ModelState));
                }

                var applicantDocument = await _context.ApplicantDocument.Where(x => x.FkApplicantId == applicantId).ToListAsync();

                if (applicantDocument == null)
                {
                    return new APIResponse(404, $"Could not find applicants documents with id of {applicantId}.");
                }

                return new APIResponse(200, $"Applicant Documents found.", applicantDocument);
            }
            catch (SystemException ex)
            {
                return new APIResponse(500, "Server Error", ex.Message);
            }

        }

        // POST: api/ApplicantDocuments
        [HttpPost("{id}")]
        public async Task<APIResponse> PostApplicantDocument([FromRoute] int id, [FromBody] ApplicantDocument applicantDocument)
        {
            var applicantId = id;
            try
            {
                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, $"Validation Error!", ModelStateExtension.AllErrors(ModelState));
                }

                var aData = new ApplicantDocument()
                {
                    DocumentFormat = applicantDocument.DocumentFormat,
                    DocumentName = applicantDocument.DocumentName,
                    DocumentPath = applicantDocument.DocumentPath,
                    DocumentType = applicantDocument.DocumentType,
                    CreatedAt = DateTime.Now,
                    FkApplicantId = applicantId
                };

                await _context.ApplicantDocument.AddAsync(aData);
                await _context.SaveChangesAsync();
                return new APIResponse(200, $"Success!", aData);

            }
            catch (SystemException ex)
            {
                return new APIResponse(500, "Server Error", ex.Message);
            }
        }

        // DELETE: api/ApplicantDocuments/5
        [HttpDelete("{id}")]
        public async Task<APIResponse> DeleteApplicantDocument([FromRoute] int id)
        {
            var applicantDocumentId = id;
            try
            {

                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, "Validation Error!", ModelStateExtension.AllErrors(ModelState));
                }
                var aData = new ApplicantDocument()
                {
                    Id = applicantDocumentId
                };
                _context.ApplicantDocument.Remove(aData);
                await _context.SaveChangesAsync();
                return new APIResponse(200, $"Success! Deleted record with id {applicantDocumentId}", aData);
            }
            catch (SystemException ex)
            {
                return new APIResponse(500, "Server Error!", ex.Message);
            }
        }

        private bool ApplicantDocumentExists(int id)
        {
            return _context.ApplicantDocument.Any(e => e.Id == id);
        }
    }
}