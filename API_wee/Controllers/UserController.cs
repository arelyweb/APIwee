using API_wee.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_wee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
       
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<ApplicationUser> Get()
        {
            var userWithA = _context.User.Where(s => s.LastName == "Gonzalez").ToList(); // Query executed by .ToList()

            return userWithA;
        }

        // GET api/<UserController>/5
        [HttpGet("{userName}")]
        public string Get(string userName)
        {
            return "value";
        }

        //// POST api/<UserController>/authenticate
        //[HttpPost]
        //[Route("authenticate")]
        //public IHttpActionResult Authenticate([FromBody] string value)
        //{
        //}

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
