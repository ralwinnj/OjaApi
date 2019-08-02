using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BcmmOja.Models;
using VMD.RESTApiResponseWrapper.Core.Extensions;
using VMD.RESTApiResponseWrapper.Core.Wrappers;

namespace BcmmOja.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantVacanciesController : ControllerBase
    {
        private readonly bcmm_ojaContext _context;

        public ApplicantVacanciesController(bcmm_ojaContext context)
        {
            _context = context;
        }

        // GET: api/ApplicantVacancies
        [HttpGet]
        public APIResponse GetApplicantVacancy()
        {
            return new APIResponse(200, $"Success!", _context.ApplicantVacancy);
        }

        // GET: api/ApplicantVacancies/5
        [HttpGet("{id}")]
        public async Task<APIResponse> GetApplicantVacancy([FromRoute] int id)
        {
            var applicantId = id;

            try
            {
                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, $"Validation error.", ModelStateExtension.AllErrors(ModelState));
                }

                var applicantVacancy = await _context.ApplicantVacancy.Where(x => x.FkApplicantId == applicantId).ToListAsync();

                if (applicantVacancy == null)
                {
                    return new APIResponse(404, $"Could not find applicants vacancy with id of {applicantId}.");
                }

                return new APIResponse(200, $"Applicant vacancies found.", applicantVacancy);

            }
            catch (SystemException ex)
            {
                return new APIResponse(500, $"Server Error", ex.Message);
            }
        }

        // POST: api/ApplicantVacancies/5
        [HttpPost("{id}")]
        public async Task<APIResponse> PostApplicantVacancy([FromRoute] int id, [FromBody] ApplicantVacancy applicantVacancy)
        {
            var applicantId = id;
            try
            {
                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, $"Validation Error!", ModelStateExtension.AllErrors(ModelState));
                }

                var aData = new ApplicantVacancy()
                {
                    Title = applicantVacancy.Title,
                    Directorate = applicantVacancy.Directorate,
                    Grade = applicantVacancy.Grade,
                    Package = applicantVacancy.Package,
                    Reference = applicantVacancy.Reference,
                    Requirements = applicantVacancy.Requirements,
                    Kpas = applicantVacancy.Kpas,
                    Date = applicantVacancy.Date,
                    ClosingDate = applicantVacancy.ClosingDate,
                    Download = applicantVacancy.Download,
                    Contact = applicantVacancy.Contact,
                    Author = applicantVacancy.Author,
                    Active = applicantVacancy.Active,
                    Count = applicantVacancy.Count,
                    Day = applicantVacancy.Day,
                    Month = applicantVacancy.Month,
                    Year = applicantVacancy.Year,
                    FkApplicantId = applicantId,
                };

                await _context.ApplicantVacancy.AddAsync(aData);
                await _context.SaveChangesAsync();
                return new APIResponse(200, $"Success!", aData);

            }
            catch (SystemException ex)
            {
                return new APIResponse(500, "Server Error", ex.Message);
            }
        }

        private bool ApplicantVacancyExists(int id)
        {
            return _context.ApplicantVacancy.Any(e => e.Id == id);
        }
    }
}