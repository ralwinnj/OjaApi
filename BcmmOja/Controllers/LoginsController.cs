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
    public class LoginsController : ControllerBase
    {
        private readonly bcmm_ojaContext _context;

        public LoginsController(bcmm_ojaContext context)
        {
            _context = new bcmm_ojaContext();
        }

        // POST: api/Logins
        [HttpPost]
        public async Task<APIResponse> PostLogin([FromBody] Login login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, $"Validation Error!", ModelStateExtension.AllErrors(ModelState));
                }

                var loginData = _context.Login.FirstOrDefault(x => x.Email.ToLower() == login.Email.ToLower());
                if (loginData == null)
                {
                    return new APIResponse(404, $"User with email {login.Email} does not exist");
                }
                if (loginData.Password != login.Password)
                {
                    return new APIResponse(409, "Incorrect password");
                }

                await _context.LoginLog.AddAsync(
                    new LoginLog()
                    {
                        Email = loginData.Email,
                        CreatedAt = DateTime.Now,
                        FkApplicantId = loginData.FkApplicantId
                    }
                );

                loginData.LastLogin = DateTime.Now;

                await _context.SaveChangesAsync();
                loginData.Password = null;
                return new APIResponse(200, $"Success!", loginData);
            }
            catch (SystemException ex)
            {
                return new APIResponse(500, "Server Error", ex.InnerException);
            }
        }

        //// DELETE: api/Logins/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteLogin([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var login = await _context.Login.FindAsync(id);
        //    if (login == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Login.Remove(login);
        //    await _context.SaveChangesAsync();

        //    return Ok(login);
        //}

        private bool LoginExists(int id)
        {
            return _context.Login.Any(e => e.Id == id);
        }
    }
}