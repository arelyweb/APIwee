using API_wee.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_wee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PolicyController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<PolicyController>
        [HttpGet]
        public IEnumerable<Policy> Get()
        {
            
            var res= _context.Policy.Where(p=> p.Id_policy == 1).ToList();
            return res;
        }

        // GET api/<PolicyController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PolicyController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
