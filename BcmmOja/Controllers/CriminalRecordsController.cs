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
    public class CriminalRecordsController : ControllerBase
    {
        private readonly bcmm_ojaContext _context;

        public CriminalRecordsController(bcmm_ojaContext context)
        {
            _context = new bcmm_ojaContext();
        }

        // GET: api/CriminalRecords
        [HttpGet]
        public APIResponse GetCriminalRecord()
        {
            try
            {
                return new APIResponse(200, $"Success", _context.CriminalRecord);
            }
            catch (SystemException ex)
            {
                return new APIResponse(500, $"Server error", ex.InnerException);
            }
        }

        // GET: api/CriminalRecords/5
        [HttpGet("{id}")]
        public async Task<APIResponse> GetCriminalRecord([FromRoute] int id)
        {
            var applicantId = id;
            try
            {
                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, $"Validation error.", ModelStateExtension.AllErrors(ModelState));
                }

                var criminalRecord = await _context.CriminalRecord.Where(x => x.FkApplicantId == applicantId).ToListAsync();

                if (criminalRecord == null)
                {
                    return new APIResponse(404, $"Could not find a criminal records with id of {applicantId}.");
                }

                return new APIResponse(200, $"Criminal records found.", criminalRecord);
            }
            catch (SystemException ex)
            {
                return new APIResponse(500, "Server Error", ex.InnerException);
            }
        }

        // PUT: api/CriminalRecords/5
        [HttpPut("{id}")]
        public async Task<APIResponse> PutCriminalRecord([FromRoute] int id, [FromBody] CriminalRecord criminalRecord)
        {
            try
            {
                if (!CriminalRecordExists(id))
                {
                    return new APIResponse(404, $"Criminal Record not found!", id);
                }

                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, $"Validation error!", ModelStateExtension.AllErrors(ModelState));
                }

                if (id != criminalRecord.Id)
                {
                    return new APIResponse(409, $"Supplied id {id} does not match with the one in our records {criminalRecord.Id}.", ModelStateExtension.AllErrors(ModelState));
                }

                _context.Entry(criminalRecord).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return new APIResponse(200, $"Applicant details updated successfully.", criminalRecord);
            }
            catch (SystemException ex)
            {
                return new APIResponse(500, $"Server Error!", ex.InnerException);
            }
        }

        // POST: api/CriminalRecords
        [HttpPost("{id}")]
        public async Task<APIResponse> PostCriminalRecord([FromRoute] int id, [FromBody] CriminalRecord criminalRecord)
        {
            var applicantId = id;
            try
            {
                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, $"Validation Error!", ModelStateExtension.AllErrors(ModelState));
                }

                var aData = new CriminalRecord()
                {   
                    Record = true,
                    TypeOfCriminalAct = criminalRecord.TypeOfCriminalAct,
                    DateFinalized = criminalRecord.DateFinalized,
                    Outcome = criminalRecord.Outcome,
                    CreatedAt = criminalRecord.CreatedAt ?? DateTime.Now,
                    FkApplicantId = applicantId
                };

                await _context.CriminalRecord.AddAsync(aData);
                await _context.SaveChangesAsync();
                return new APIResponse(200, $"Success!", aData);

            }
            catch (SystemException ex)
            {
                return new APIResponse(500, "Server Error", ex.InnerException);
            }
        }

        // DELETE: api/CriminalRecords/5
        [HttpDelete("{id}")]
        public async Task<APIResponse> DeleteCriminalRecord([FromRoute] int id)
        {
            var criminalRecordId = id;
            try
            {
                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, "Validation Error!", ModelStateExtension.AllErrors(ModelState));
                }
                var aData = new CriminalRecord()
                {
                    Id = criminalRecordId
                };
                _context.CriminalRecord.Remove(aData);
                await _context.SaveChangesAsync();
                return new APIResponse(200, $"Success! Deleted record with id {criminalRecordId}", aData);
            }
            catch (SystemException ex)
            {
                return new APIResponse(500, "Server Error!", ex.InnerException);
            }
        }

        private bool CriminalRecordExists(int id)
        {
            return _context.CriminalRecord.Any(e => e.Id == id);
        }
    }
}