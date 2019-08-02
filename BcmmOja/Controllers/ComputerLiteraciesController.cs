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
    public class ComputerLiteraciesController : ControllerBase
    {
        private readonly bcmm_ojaContext _context;

        public ComputerLiteraciesController(bcmm_ojaContext context)
        {
            _context = new bcmm_ojaContext();
        }

        // GET: api/ComputerLiteracies
        [HttpGet]
        public APIResponse GetComputerLiteracy()
        {
            try
            {
                return new APIResponse(200, $"Success!", _context.ComputerLiteracy);
            }
            catch (SystemException ex)
            {
                return new APIResponse(500, $"Serve error!", ex.Message);
            }
        }

        // GET: api/ComputerLiteracies/5
        [HttpGet("{id}")]
        public async Task<APIResponse> GetComputerLiteracy([FromRoute] int id)
        {
            var applicantId = id;
            try
            {
                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, $"Validation error.", ModelStateExtension.AllErrors(ModelState));
                }

                var computerLiteracy = await _context.ComputerLiteracy.Where(x => x.FkApplicantId == applicantId).ToListAsync();

                if (computerLiteracy == null)
                {
                    return new APIResponse(404, $"Could not find applicants computer literacies with id of {applicantId}.");
                }

                return new APIResponse(200, $"Computer literacies found.", computerLiteracy);
            }
            catch (SystemException ex)
            {
                return new APIResponse(500, "Server Error", ex.Message);
            }
        }

        // POST: api/ComputerLiteracies
        [HttpPost("{id}")]
        public async Task<APIResponse> PostComputerLiteracy([FromRoute] int id, [FromBody] ComputerLiteracy body)
        {
            var applicantId = id;
            try
            {
                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, $"Validation error!", ModelStateExtension.AllErrors(ModelState));
                }

                var aData = new ComputerLiteracy()
                {
                    Skill = body.Skill,
                    Competency = body.Competency,
                    CreatedAt = DateTime.Now,
                    FkApplicantId = applicantId
                };

                await _context.ComputerLiteracy.AddAsync(aData);
                await _context.SaveChangesAsync();
                return new APIResponse(200, $"Success!", aData);
            }
            catch (SystemException ex)
            {
                return new APIResponse(500, $"Server error!", ex.Message);
            };

        }

        // DELETE: api/ComputerLiteracies/5
        [HttpDelete("{id}")]
        public async Task<APIResponse> DeleteComputerLiteracy([FromRoute] int id)
        {
            var compunterLiteracyId = id;
            try
            {

                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, "Validation Error!", ModelStateExtension.AllErrors(ModelState));
                }
                var aData = new ComputerLiteracy()
                {
                    Id = compunterLiteracyId
                };
                _context.ComputerLiteracy.Remove(aData);
                await _context.SaveChangesAsync();
                return new APIResponse(200, $"Success! Deleted record with id {compunterLiteracyId}", aData);
            }
            catch (SystemException ex)
            {
                return new APIResponse(500, "Server Error!", ex.Message);
            }
        }

        private bool ComputerLiteracyExists(int id)
        {
            return _context.ComputerLiteracy.Any(e => e.Id == id);
        }
    }
}