using Microsoft.AspNetCore.Mvc;
using BcmmOja.Utility;
using Microsoft.Extensions.Options;
using BcmmOja.Models;
using VMD.RESTApiResponseWrapper.Core.Wrappers;

namespace BcmmOja.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly EmailSettings _mailSettings;
        private readonly bcmm_ojaContext _context;
        public ValuesController(IOptions<EmailSettings> mailSettings)
        {
            _context = new bcmm_ojaContext();
            _mailSettings = mailSettings.Value;
        }
        // GET api/values
        [HttpGet]
        public APIResponse Get()
        {
            return new APIResponse(200, "GET api/values Success", new string[] { "value 1", "value 2" });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public APIResponse Get(int id)
        {
            return new APIResponse(200, "GET api/values/:id Success", new string[] { "value 1", $"Your id is: {id}" });
        }

        // POST api/values
        [HttpPost]
        public APIResponse Post([FromBody] string value)
        {
            return new APIResponse(200, "POST api/values Success");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public APIResponse Put(int id, [FromBody] string value)
        {
            return new APIResponse(200, "PUT api/values Success", $"Your id is: {id}");
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public APIResponse Delete(int id)
        {
            return new APIResponse(200, "DELETE api/values Success", $"Your id is: {id}");
        }
    }
}
