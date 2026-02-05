using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_wee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpGet("secret")]
        public IActionResult Secret() => Ok("Solo Admin puede ver esto");

        [Authorize(Roles = "User,Admin")]
        [HttpGet("user-area")]
        public IActionResult UserArea() => Ok("Usuario logueado con rol User o Admin");
    }
}
