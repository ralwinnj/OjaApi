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
    public class ExperiencesController : ControllerBase
    {
        private readonly bcmm_ojaContext _context;

        public ExperiencesController(bcmm_ojaContext context)
        {
            _context = new bcmm_ojaContext();
        }

        // GET: api/Experiences
        [HttpGet]
        public APIResponse GetExperience()
        {
            try
            {
                return new APIResponse(200, "Success!", _context.Experience);
            }
            catch (SystemException ex)
            {
                return new APIResponse(500, "Server Error!", ex.InnerException);
            }
        }

        // GET: api/Experiences/5
        [HttpGet("{id}")]
        public async Task<APIResponse> GetExperience([FromRoute] int id)
        {
            var applicantId = id;
            try
            {

                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, "Validation Error", ModelStateExtension.AllErrors(ModelState));
                }

                var experience = await _context.Experience.Where(x => x.FkApplicantId == applicantId).ToListAsync();

                if (experience == null)
                {
                    return new APIResponse(404, $"Could not find a experience record with id of {applicantId}.");
                }

                return new APIResponse(200, $"Experience records found.", experience);
            }
            catch (System.Exception ex)
            {
                return new APIResponse(500, "Server error!", ex.InnerException);
            }

        }

        // PUT: api/Experiences/5
        [HttpPut("{id}")]
        public async Task<APIResponse> PutExperience([FromRoute] int id, [FromBody] Experience experience)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, "Validation Error!", ModelStateExtension.AllErrors(ModelState));
                }
                if (!ExperienceExists(id))
                {
                    return new APIResponse(404, "Not found!");
                }
                if (id != experience.Id)
                {
                    return new APIResponse(409, $"Supplied id {id} does not match with the one in our records {experience.Id}.", ModelStateExtension.AllErrors(ModelState));
                }


                _context.Entry(experience).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return new APIResponse(200, $"Experience details updated successfully.", experience);
            }
            catch (System.Exception ex)
            {
                return new APIResponse(500, "Server Error!", ex.InnerException);
            }
        }

        // POST: api/Experiences
        [HttpPost("{id}")]
        public async Task<APIResponse> PostExperience([FromRoute] int id, [FromBody] Experience experience)
        {
            var applicantId = id;
            try
            {
                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, $"Validation Error!", ModelStateExtension.AllErrors(ModelState));
                }

                var aData = new Experience()
                {
                    Employer = experience.Employer,
                    Position = experience.Position,
                    StartDate = experience.StartDate,
                    EndDate = experience.EndDate,
                    ReasonForLeaving = experience.ReasonForLeaving,
                    Description = experience.Description,
                    PreviousMunicipality = experience.PreviousMunicipality,
                    PreviousMunicipalityName = experience.PreviousMunicipalityName,
                    CreatedAt = DateTime.Now,
                    FkApplicantId = applicantId
                };

                await _context.Experience.AddAsync(aData);
                await _context.SaveChangesAsync();
                return new APIResponse(200, $"Success!", aData);

            }
            catch (SystemException ex)
            {
                return new APIResponse(500, "Server Error", ex.InnerException);
            }
        }

        // DELETE: api/Experiences/5
        [HttpDelete("{id}")]
        public async Task<APIResponse> DeleteExperience([FromRoute] int id)
        {

            var experienceRecordId = id;
            try
            {
                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, "Validation Error!", ModelStateExtension.AllErrors(ModelState));
                }

                var experience = await _context.Experience.FindAsync(id);

                if (experience == null)
                {
                    return new APIResponse(404, $"Not found! {experienceRecordId}");
                }

                _context.Experience.Remove(experience);

                await _context.SaveChangesAsync();

                return new APIResponse(200, $"Success! Deleted record with id {experienceRecordId}");
            }
            catch (SystemException ex)
            {
                return new APIResponse(500, "Server Error!", ex.InnerException);
            }





        }

        private bool ExperienceExists(int id)
        {
            return _context.Experience.Any(e => e.Id == id);
        }
    }
}