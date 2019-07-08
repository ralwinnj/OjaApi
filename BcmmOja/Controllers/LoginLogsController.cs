using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BcmmOja.Models;

namespace BcmmOja.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginLogsController : ControllerBase
    {
        private readonly bcmm_ojaContext _context;

        public LoginLogsController(bcmm_ojaContext context)
        {
            _context = context;
        }

        // GET: api/LoginLogs
        [HttpGet]
        public IEnumerable<LoginLog> GetLoginLog()
        {
            return _context.LoginLog;
        }

        // GET: api/LoginLogs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLoginLog([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loginLog = await _context.LoginLog.FindAsync(id);

            if (loginLog == null)
            {
                return NotFound();
            }

            return Ok(loginLog);
        }

        // PUT: api/LoginLogs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoginLog([FromRoute] int id, [FromBody] LoginLog loginLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != loginLog.Id)
            {
                return BadRequest();
            }

            _context.Entry(loginLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoginLogExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/LoginLogs
        [HttpPost]
        public async Task<IActionResult> PostLoginLog([FromBody] LoginLog loginLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.LoginLog.Add(loginLog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLoginLog", new { id = loginLog.Id }, loginLog);
        }

        // DELETE: api/LoginLogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoginLog([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loginLog = await _context.LoginLog.FindAsync(id);
            if (loginLog == null)
            {
                return NotFound();
            }

            _context.LoginLog.Remove(loginLog);
            await _context.SaveChangesAsync();

            return Ok(loginLog);
        }

        private bool LoginLogExists(int id)
        {
            return _context.LoginLog.Any(e => e.Id == id);
        }
    }
}