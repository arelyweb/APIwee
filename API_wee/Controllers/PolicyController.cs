using API_wee.Models;
using API_wee.Repositories;
using API_wee.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_wee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyService _policyService;
        private readonly IClientService _clientService;
        public PolicyController( IPolicyService policyService, IClientService clientService)
        {
            _policyService = policyService;
            _clientService = clientService;
        }
        // GET: api/<PolicyController>
        [HttpGet("client/{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
          var client = await _clientService.GetClientByIdAsync(id);
            if (client == null) return NotFound();
            return Ok(client);
        }
        // GET: api/<PolicyController>
        [HttpGet("client")]
        public async Task<IActionResult> GetClient()
        {
            var client = await _clientService.GetClientAsync();
            if (client == null) return NotFound();
            return Ok(client);
        }
        // GET api/<PolicyController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPolicyById(int id)
        {
            var policy = await _policyService.GetPolicyByIdAsync(id);
            if (policy == null) return NotFound();
            return Ok(policy);

        }

        // POST api/<PolicyController>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Policy value)
        {
            //guardar poliza
            var created =  await _policyService.CreatePolicyAsync(value.Id_client, value.Id_typePolicy, value.id_statusPolicy, value.numPolicy, value.startDatePolicy, value.endDatePolicy, value.amountPolicy);
            return CreatedAtAction(nameof(Create), new { id = created.Id_policy }, created);

        }

        // PUT api/<PolicyController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PolicyController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
