using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BcmmOja.Models;
using BcmmOja.Services;
using VMD.RESTApiResponseWrapper.Core.Wrappers;
using VMD.RESTApiResponseWrapper.Core.Extensions;
using Microsoft.Extensions.Options;
using BcmmOja.Utility;

namespace BcmmOja.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtpsController : ControllerBase
    {
        private readonly bcmm_ojaContext _context;
         private readonly IOptions<EmailSettings> _mailSettings;

        public OtpsController(IOptions<EmailSettings> mailSettings)
        {

            _context = new bcmm_ojaContext();
            _mailSettings = mailSettings;
        }

        // PUT: api/Otps/5
        // Use this endpoint to compare the otp sent
        [HttpPut("{id}")]
        public async Task<APIResponse> PutOtp([FromRoute] int id, [FromBody] OtpPut otpPut)
        {
            // id = otp Id
            try
            {
                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, $"Validation error!", ModelStateExtension.AllErrors(ModelState));
                }

                if (OtpExists(id))
                {
                    // success logic for when a OTP record has been found
                    var otpPost = await _context.Otp.FindAsync(id);

                    // check if otp we have on record matches with the one supplied
                    if (otpPost.OtpSentValue == otpPut.OtpReceivedValue)
                    {
                        // return success 200 here
                        otpPost.OtpSentVerified = true;
                        var login = await _context.Login.FirstOrDefaultAsync(x => x.FkApplicantId == otpPost.OtpSentToId);
                        login.IsVerified = true;
                        await _context.SaveChangesAsync();

                        return new APIResponse(200, "The OTP matched!");
                    }
                    else
                    {
                        // return failure 409 here (otp does not match);
                        return new APIResponse(409, "The OTP does not match!");
                    }
                }
                else
                {
                    return new APIResponse(404, "Could not find a record of this OTP!");
                }

            }
            catch (System.Exception ex)
            {
                return new APIResponse(500, "Server Error.", ex.Message);
            }

        }

        // POST: api/Otps
        [HttpPost]
        public async Task<APIResponse> PostOtp([FromBody] Otp otpPost)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return new APIResponse(400, $"Validation Error!", ModelStateExtension.AllErrors(ModelState));
                }

                Random generator = new Random();
                int r = generator.Next(100000, 1000000);

                var aData = new Otp()
                {
                    OtpSentVia = otpPost.OtpSentVia,
                    OtpSentValue = r.ToString(),
                    OtpSentVerified = false,
                    OtpSentToId = otpPost.OtpSentToId
                };

                await _context.Otp.AddAsync(aData);
                await _context.SaveChangesAsync();

                var applicant = await _context.Applicant.FindAsync(aData.OtpSentToId);
                var login = await _context.Login.FirstOrDefaultAsync(x => x.FkApplicantId == aData.OtpSentToId);
                var name = $"{applicant.FirstName} {applicant.LastName}";
                var email = login.Email;
                var subject = "Buffalocity OTP Service";
                var mailBody = $"Your OTP is: {aData.OtpSentValue}";

                var emailSender = new EmailSender(_mailSettings);
                var emailSent = await emailSender.SendEmail(name, email, subject, mailBody);

                if (emailSent)
                {
                    return new APIResponse(200, $"Success! Email Sent.", aData);
                }
                else
                {
                    return new APIResponse(200, $"Fail! Email could not be sent, please try again.", aData);
                }

            }
            catch (SystemException ex)
            {
                Console.WriteLine(ex);
                return new APIResponse(500, "Server Error", ex.Message);
            }

        }

        // DELETE: api/Otps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOtp([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var otp = await _context.Otp.FindAsync(id);
            if (otp == null)
            {
                return NotFound();
            }

            _context.Otp.Remove(otp);
            await _context.SaveChangesAsync();

            return Ok(otp);
        }

        private bool OtpExists(int id)
        {
            return _context.Otp.Any(e => e.Id == id);
        }
    }
}