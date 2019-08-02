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
    public class QualificationsController : ControllerBase
    {
        private readonly bcmm_ojaContext _context;

        public QualificationsController(bcmm_ojaContext context)
        {
            _context = new bcmm_ojaContext();
        }

        // GET: api/Qualifications
        [HttpGet]
        public APIResponse GetQualification()
        {
            try
            {
                return new APIResponse(200, "Success!", _context.Qualification);
            }
            catch (SystemException ex)
            {
                return new APIResponse(500, "Server Error!", ex.Message);
            }
        }

        // GET: api/Qualifications/5
        [HttpGet("{id}")]
        public async Task<APIResponse> GetQualification([FromRoute] int id)
        {
            var applicantId = id;
            try
            {

                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, "Validation Error", ModelStateExtension.AllErrors(ModelState));
                }

                var qualification = await _context.Qualification.Where(x => x.FkApplicantId == applicantId).ToListAsync();

                if (qualification == null)
                {
                    return new APIResponse(404, $"Could not find qualification(s) record with id of {applicantId}.");
                }

                return new APIResponse(200, $"Qualification(s) records found.", qualification);
            }
            catch (System.Exception ex)
            {
                return new APIResponse(500, "Server error!", ex.Message);
            }
        }

        // PUT: api/Qualifications/5
        [HttpPut("{id}")]
        public async Task<APIResponse> PutQualification([FromRoute] int id, [FromBody] Qualification qualification)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, "Validation Error!", ModelStateExtension.AllErrors(ModelState));
                }
                if (!QualificationExists(id))
                {
                    return new APIResponse(404, "Not found!");
                }
                if (id != qualification.Id)
                {
                    return new APIResponse(409, $"Supplied id {id} does not match with the one in our records {qualification.Id}.", ModelStateExtension.AllErrors(ModelState));
                }


                _context.Entry(qualification).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return new APIResponse(200, $"Qualification details updated successfully.", qualification);
            }
            catch (System.Exception ex)
            {
                return new APIResponse(500, "Server Error!", ex.Message);
            }

        }

        // POST: api/Qualifications
        [HttpPost("{id}")]
        public async Task<APIResponse> PostQualification([FromRoute] int id, [FromBody] Qualification qualification)
        {
            var applicantId = id;
            try
            {
                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, $"Validation Error!", ModelStateExtension.AllErrors(ModelState));
                }

                var aData = new Qualification()
                {
                    NameOfInstitute = qualification.NameOfInstitute,
                    NameOfQualification = qualification.NameOfQualification,
                    TypeOfQualification = qualification.TypeOfQualification,
                    YearObtained = qualification.YearObtained,
                    CreatedAt = DateTime.Now,
                    FkApplicantId = applicantId
                };

                await _context.Qualification.AddAsync(aData);
                await _context.SaveChangesAsync();
                return new APIResponse(200, $"Success!", aData);

            }
            catch (SystemException ex)
            {
                return new APIResponse(500, "Server Error", ex.Message);
            }
        }

        // DELETE: api/Qualifications/5
        [HttpDelete("{id}")]
        public async Task<APIResponse> DeleteQualification([FromRoute] int id)
        {
            var qualificationId = id;
            try
            {
                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, "Validation Error!", ModelStateExtension.AllErrors(ModelState));
                }

                var qualification = await _context.Qualification.FindAsync(qualificationId);

                if (qualification == null)
                {
                    return new APIResponse(404, $"Not found! {qualificationId}");
                }

                _context.Qualification.Remove(qualification);

                await _context.SaveChangesAsync();

                return new APIResponse(200, $"Success! Deleted record with id {qualificationId}");
            }
            catch (SystemException ex)
            {
                return new APIResponse(500, "Server Error!", ex.Message);
            }
        }

        private bool QualificationExists(int id)
        {
            return _context.Qualification.Any(e => e.Id == id);
        }
    }
}