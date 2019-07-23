using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BcmmOja.Models;
using BcmmOja.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VMD.RESTApiResponseWrapper.Core.Extensions;
using VMD.RESTApiResponseWrapper.Core.Wrappers;

namespace BcmmOja.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly bcmm_ojaContext _context;


        public UsersController(bcmm_ojaContext context)
        {
            _context = new bcmm_ojaContext();
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<APIResponse> PutApplicant([FromRoute] int id, [FromBody] Applicant applicant)
        {
            if (!ModelState.IsValid)
            {
                return new APIResponse(400, $"Validation error", ModelStateExtension.AllErrors(ModelState));
            }

            if (id != applicant.Id)
            {
                return new APIResponse(401, $"Supplied id {id} does not match with the one in our records {applicant.Id}.", ModelStateExtension.AllErrors(ModelState));
            }

            _context.Entry(applicant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicantExists(id))
                {
                    return new APIResponse(404, $"Applicant not found.", id);
                    //return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return new APIResponse(200, $"Applicant details updated successfully.");
            // return NoContent();
        }

        // POST: api/Applicants
        [HttpPost]
        public async Task<APIResponse> PostApplicant([FromBody] RegisterModel applicant)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, "Validation error.", ModelStateExtension.AllErrors(ModelState));
                }
                if (await _context.Applicant.AnyAsync(x => x.IdNumber == applicant.IdNumber))
                {
                    return new APIResponse(300, $"A user already exists with the ID number {applicant.IdNumber}.", applicant);
                };
                if (await _context.Applicant.AnyAsync(x => x.PhoneNumber == applicant.PhoneNumber))
                {
                    return new APIResponse(300, $"A user already exists with the phone number {applicant.PhoneNumber}.", applicant);
                }

                if (applicant != null)
                {
                    if (await _context.Login.AnyAsync(x => x.Email == applicant.Email))
                    {
                        return new APIResponse(300, $"A user already exists with the email address {applicant.Email}", applicant);
                    }
                }

                var aData = new Applicant()
                {
                    FirstName = applicant.FirstName,
                    LastName = applicant.LastName,                    
                    Citizenship = applicant.Citizenship,
                    IdNumber = applicant.IdNumber,
                    PhoneNumber = applicant.PhoneNumber,
                };

                await _context.Applicant.AddAsync(aData);
                await _context.SaveChangesAsync();

                var newApplicant = await _context.Applicant.FirstOrDefaultAsync(x => x.IdNumber == applicant.IdNumber);
                var aData2 = new Login()
                {
                    Email = applicant.Email,
                    Password = applicant.Password,
                    FkApplicantId = newApplicant.Id
                };
                await _context.Login.AddAsync(aData2);
                await _context.SaveChangesAsync();



                return new APIResponse(200, "User created successfully", applicant);
            }
            catch (System.Exception ex)
            {
                return new APIResponse(500, "Server Error", ex.InnerException);
            }
        }

        // DELETE: api/Applicants/5
        [HttpDelete("{id}")]
        public async Task<APIResponse> DeleteApplicant([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return new APIResponse(400, "Validation error.", ModelStateExtension.AllErrors(ModelState));
            }

            var applicant = await _context.Applicant.FindAsync(id);
            if (applicant == null)
            {
                return new APIResponse(404, $"User with id {id}, not found.");
            }

            var login = await _context.Login.FirstAsync(x => x.FkApplicantId == id);
            if (login != null)
            {
                _context.Login.Remove(login);
            }

            _context.Applicant.Remove(applicant);

            await _context.SaveChangesAsync();

            return new APIResponse(200, $"User successfully deleted", applicant);
        }

        private bool ApplicantExists(int id)
        {
            return _context.Applicant.Any(e => e.Id == id);
        }
    }
}