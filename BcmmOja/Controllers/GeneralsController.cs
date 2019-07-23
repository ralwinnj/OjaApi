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
    public class GeneralsController : ControllerBase
    {
        private readonly bcmm_ojaContext _context;

        public GeneralsController(bcmm_ojaContext context)
        {
            _context = new bcmm_ojaContext();
        }

        // GET: api/Generals
        [HttpGet]
        public APIResponse GetGeneral()
        {
            try
            {
                return new APIResponse(200, "Success", _context.General);
            }
            catch (System.Exception ex)
            {
                return new APIResponse(500, "Server Error!", ex.InnerException);
            }
        }

        // GET: api/Generals/5
        [HttpGet("{id}")]
        public async Task<APIResponse> GetGeneral([FromRoute] int id)
        {

            var applicantId = id;
            try
            {

                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, "Validation Error", ModelStateExtension.AllErrors(ModelState));
                }

                var general = await _context.General.Where(x => x.FkApplicantId == applicantId).ToListAsync();

                if (general == null)
                {
                    return new APIResponse(404, $"Could not find a genaral record with id of {applicantId}.");
                }

                return new APIResponse(200, $"General records found.", general);
            }
            catch (System.Exception ex)
            {
                return new APIResponse(500, "Server error!", ex.InnerException);
            }

        }

        // PUT: api/Generals/5
        [HttpPut("{id}")]
        public async Task<APIResponse> PutGeneral([FromRoute] int id, [FromBody] General general)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, "Validation Error!", ModelStateExtension.AllErrors(ModelState));
                }
                if (!GeneralExists(id))
                {
                    return new APIResponse(404, "Not found!");
                }
                if (id != general.Id)
                {
                    return new APIResponse(409, $"Supplied id {id} does not match with the one in our records {general.Id}.", ModelStateExtension.AllErrors(ModelState));
                }

                _context.Entry(general).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return new APIResponse(200, $"General details updated successfully.", general);
            }
            catch (System.Exception ex)
            {
                return new APIResponse(500, "Server Error!", ex.InnerException);
            }
        }

        // POST: api/Generals
        [HttpPost("{id}")]
        public async Task<APIResponse> PostGeneral([FromRoute] int id, [FromBody] General general)
        {

            var applicantId = id;
            try
            {
                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, $"Validation Error!", ModelStateExtension.AllErrors(ModelState));
                }

                var aData = new General()
                {
                    PhysicalMentalCondition = general.PhysicalMentalCondition,
                    ConflictOfInterest = general.ConflictOfInterest,
                    ConflictOfInterestReason = general.ConflictOfInterestReason,
                    CommenceDate = general.CommenceDate,
                    PositionTermsAccepted = general.PositionTermsAccepted,
                    CreatedAt = DateTime.Now,
                    FkApplicantId = applicantId
                };

                await _context.General.AddAsync(aData);
                await _context.SaveChangesAsync();
                return new APIResponse(200, $"Success!", aData);
            }
            catch (SystemException ex)
            {
                return new APIResponse(500, "Server Error", ex.InnerException);
            }

        }

        // DELETE: api/Generals/5
        [HttpDelete("{id}")]
        public async Task<APIResponse> DeleteGeneral([FromRoute] int id)
        {
            var generalId = id;
            try
            {
                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, "Validation Error!", ModelStateExtension.AllErrors(ModelState));
                }

                var generalRec = await _context.General.FindAsync(generalId);

                if (generalRec == null)
                {
                    return new APIResponse(404, $"Not found! {generalId}");
                }

                _context.General.Remove(generalRec);

                await _context.SaveChangesAsync();

                return new APIResponse(200, $"Success! Deleted record with id {generalId}", generalRec);
            }
            catch (SystemException ex)
            {
                return new APIResponse(500, "Server Error!", ex.InnerException);
            }
        }

        private bool GeneralExists(int id)
        {
            return _context.General.Any(e => e.Id == id);
        }
    }
}