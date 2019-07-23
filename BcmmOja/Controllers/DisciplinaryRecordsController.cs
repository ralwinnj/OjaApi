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
    public class DisciplinaryRecordsController : ControllerBase
    {
        private readonly bcmm_ojaContext _context;

        public DisciplinaryRecordsController(bcmm_ojaContext context)
        {
            _context = new bcmm_ojaContext();
        }

        // GET: api/DisciplinaryRecords
        [HttpGet]
        public APIResponse GetDisciplinaryRecord()
        {
            try
            {
                return new APIResponse(200, "Success!", _context.DisciplinaryRecord);
            }
            catch (SystemException ex)
            {
                return new APIResponse(500, "Server Error!", ex.InnerException);
            }
        }

        // GET: api/DisciplinaryRecords/5
        [HttpGet("{id}")]
        public async Task<APIResponse> GetDisciplinaryRecord([FromRoute] int id)
        {
            var applicantId = id;
            try
            {
                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, "Validation Error", ModelStateExtension.AllErrors(ModelState));
                }
                var disciplinaryRecord = await _context.DisciplinaryRecord.Where(x => x.FkApplicantId == applicantId).ToListAsync();

                if (disciplinaryRecord == null)
                {
                    return new APIResponse(404, $"Could not find a disciplinary record with id of {applicantId}.");
                }

                return new APIResponse(200, $"Disciplinary records found.", disciplinaryRecord);
            }
            catch (System.Exception ex)
            {
                return new APIResponse(500, "Server error!", ex.InnerException);
            }
           
        }

        // PUT: api/DisciplinaryRecords/5
        [HttpPut("{id}")]
        public async Task<APIResponse> PutDisciplinaryRecord([FromRoute] int id, [FromBody] DisciplinaryRecord disciplinaryRecord)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, "Validation Error!", ModelStateExtension.AllErrors(ModelState));
                }
                if (id != disciplinaryRecord.Id)
                {
                    return new APIResponse(409, $"Supplied id {id} does not match with the one in our records {disciplinaryRecord.Id}.", ModelStateExtension.AllErrors(ModelState));
                }

                _context.Entry(disciplinaryRecord).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return new APIResponse(200, $"Disciplinary record details updated successfully.", disciplinaryRecord);
            }
            catch (System.Exception ex)
            {
                return new APIResponse(500, "Server Error!", ex.InnerException);
            }


            
        }

        // POST: api/DisciplinaryRecords
        [HttpPost("{id}")]
        public async Task<APIResponse> PostDisciplinaryRecord([FromRoute] int id, [FromBody] DisciplinaryRecord disciplinaryRecord)
        {
            var applicantId = id;
            try
            {
                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, $"Validation Error!", ModelStateExtension.AllErrors(ModelState));
                }

                var aData = new DisciplinaryRecord()
                {
                    Record = true,
                    NameOfInstitute = disciplinaryRecord.NameOfInstitute,
                    TypeOfMisconduct = disciplinaryRecord.TypeOfMisconduct,
                    DateFinalized = disciplinaryRecord.DateFinalized,
                    AwardSanction = disciplinaryRecord.AwardSanction,
                    Resign = disciplinaryRecord.Resign,
                    ResignReason = disciplinaryRecord.ResignReason,
                    CreatedAt = DateTime.Now,
                    FkApplicantId = applicantId
                };

                await _context.DisciplinaryRecord.AddAsync(aData);
                await _context.SaveChangesAsync();
                return new APIResponse(200, $"Success!", aData);

            }
            catch (SystemException ex)
            {
                return new APIResponse(500, "Server Error", ex.InnerException);
            }
        }

        // DELETE: api/DisciplinaryRecords/5
        [HttpDelete("{id}")]
        public async Task<APIResponse> DeleteDisciplinaryRecord([FromRoute] int id)
        {
            var disciplinaryRecordId = id;
            try
            {
                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, "Validation Error!", ModelStateExtension.AllErrors(ModelState));
                }
                var disciplinaryRecord = await _context.DisciplinaryRecord.FindAsync(id);

                if (disciplinaryRecord == null)
                {
                    return new APIResponse(404, $"Not found! {disciplinaryRecordId}");
                };
                _context.DisciplinaryRecord.Remove(disciplinaryRecord);
                await _context.SaveChangesAsync();
                return new APIResponse(200, $"Success! Deleted record with id {disciplinaryRecordId}", disciplinaryRecord);
            }
            catch (SystemException ex)
            {
                return new APIResponse(500, "Server Error!", ex.InnerException);
            }
        }

        private bool DisciplinaryRecordExists(int id)
        {
            return _context.DisciplinaryRecord.Any(e => e.Id == id);
        }
    }
}