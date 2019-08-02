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
    public class ProfessionalMembershipsController : ControllerBase
    {
        private readonly bcmm_ojaContext _context;

        public ProfessionalMembershipsController(bcmm_ojaContext context)
        {
            _context = new bcmm_ojaContext();
        }

        // GET: api/ProfessionalMemberships
        [HttpGet]
        public APIResponse GetProfessionalMembership()
        {
            try
            {
                return new APIResponse(200, "Success", _context.ProfessionalMembership);
            }
            catch (System.Exception ex)
            {
                return new APIResponse(500, "Server Error!", ex.Message);
            }
        }

        // GET: api/ProfessionalMemberships/5
        [HttpGet("{id}")]
        public async Task<APIResponse> GetProfessionalMembership([FromRoute] int id)
        {
            var applicantId = id;
            try
            {
                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, "Validation Error", ModelStateExtension.AllErrors(ModelState));
                }

                var professionalMembership = await _context.ProfessionalMembership.Where(x => x.FkApplicantId == applicantId).ToListAsync();

                if (professionalMembership == null)
                {
                    return new APIResponse(404, $"Could not find a professional membership record with id of {applicantId}.");
                }
                return new APIResponse(200, $"Professional Membership records found.", professionalMembership);
            }
            catch (System.Exception ex)
            {
                return new APIResponse(500, "Server error!", ex.Message);
            }
        }

        // PUT: api/ProfessionalMemberships/5
        [HttpPut("{id}")]
        public async Task<APIResponse> PutProfessionalMembership([FromRoute] int id, [FromBody] ProfessionalMembership professionalMembership)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, "Validation Error!", ModelStateExtension.AllErrors(ModelState));
                }
                if (!ProfessionalMembershipExists(id))
                {
                    return new APIResponse(404, "Not found!");
                }
                if (id != professionalMembership.Id)
                {
                    return new APIResponse(409, $"Supplied id {id} does not match with the one in our records {professionalMembership.Id}.", ModelStateExtension.AllErrors(ModelState));
                }

                _context.Entry(professionalMembership).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return new APIResponse(200, $"Professional Membership details updated successfully.", professionalMembership);
            }
            catch (System.Exception ex)
            {
                return new APIResponse(500, "Server Error!", ex.Message);
            }
        }

        // POST: api/ProfessionalMemberships
        [HttpPost("{id}")]
        public async Task<APIResponse> PostProfessionalMembership([FromRoute] int id, [FromBody] ProfessionalMembership professionalMembership)
        {
            var applicantId = id;
            try
            {
                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, $"Validation Error!", ModelStateExtension.AllErrors(ModelState));
                }
                var aData = new ProfessionalMembership()
                {
                    ProfessionalBody = professionalMembership.ProfessionalBody,
                    MembershipNumber = professionalMembership.MembershipNumber,
                    ExpiryDate = professionalMembership.ExpiryDate,
                    CreatedAt = DateTime.Now,
                    FkApplicantId = applicantId
                };
                await _context.ProfessionalMembership.AddAsync(aData);
                await _context.SaveChangesAsync();
                return new APIResponse(200, $"Success!", aData);
            }
            catch (SystemException ex)
            {
                return new APIResponse(500, "Server Error", ex.Message);
            }
        }

        // DELETE: api/ProfessionalMemberships/5
        [HttpDelete("{id}")]
        public async Task<APIResponse> DeleteProfessionalMembership([FromRoute] int id)
        {
            var professionalMembershipId = id;
            try
            {
                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, "Validation Error!", ModelStateExtension.AllErrors(ModelState));
                }

                var professionalMembership = await _context.General.FindAsync(id);

                if (professionalMembership == null)
                {
                    return new APIResponse(404, $"Not found! {professionalMembershipId}");
                }

                _context.General.Remove(professionalMembership);

                await _context.SaveChangesAsync();

                return new APIResponse(200, $"Success! Deleted record with id {professionalMembershipId}", professionalMembership);
            }
            catch (SystemException ex)
            {
                return new APIResponse(500, "Server Error!", ex.Message);
            }
        }

        private bool ProfessionalMembershipExists(int id)
        {
            return _context.ProfessionalMembership.Any(e => e.Id == id);
        }
    }
}