using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using API_wee.Models;
using API_wee.Services;
using API_wee.Repositories;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_wee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenRepository _refreshRepo;
        private readonly IUserService _userService;

        public AuthController(ITokenService tokenService, IRefreshTokenRepository refreshRepo , IUserService userService )
        {
            _tokenService = tokenService;
            _refreshRepo = refreshRepo;
            _userService = userService;
        }
        // GET: api/<AuthController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AuthController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AuthController>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {

            var user = await _userService.AuthenticateAsync(req.NameUser, req.Password);
            if (user == null) return Unauthorized("Usuario o contraseña incorrectos.");
            // Mapear roles: si no almacenas roles como lista, mapea según id_rol
            var roles = MapRolesFromRoleId(user.RoleId);

            var appUser = new ApplicationUser
            {
                IdUser = user.IdUser,
                UserName = user.UserName,
                RoleId = user.RoleId,
                LastName = user.LastName,
                PasswordHash = user.PasswordHash,
                // si tu modelo real tiene lista de roles, úsala; aquí construimos temporalmente
                // Roles = roles
            };
            var accessToken = _tokenService.CreateAccessToken(new Models.ApplicationUser
            {
                IdUser = appUser.IdUser,
                UserName = appUser.UserName,
                LastName = appUser.LastName,
                RoleId = appUser.RoleId,
                // Si TokenService usa .Roles, asegúrate de poblarla:
                // Roles = roles
            });

            var refreshToken = _tokenService.CreateRefreshToken(user.IdUser.ToString(), daysValid: 7);

            await _refreshRepo.SaveAsync(refreshToken);

            var response = new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                ExpiresAt = DateTime.UtcNow.AddMinutes(15)
            };

            return Ok(response);
        }

        //[HttpPost("refresh")]
        //public async Task<IActionResult> Refresh([FromBody] RefreshRequest req)
        //{
        //    // 1) get principal from expired access token
        //    var principal = _tokenService.GetPrincipalFromExpiredToken(req.AccessToken);
        //    if (principal == null) return BadRequest("Invalid access token.");

        //    var userId = principal.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        //    if (userId == null) return BadRequest("Invalid token claims.");

        //    // 2) get refresh token from repo
        //    var stored = await _refreshRepo.GetByTokenAsync(req.RefreshToken);
        //    if (stored == null || stored.Id_user != userId || stored.Expires < DateTime.UtcNow || stored.Revoked)
        //        return Unauthorized("Invalid refresh token.");

        //    // 3) rotate refresh token (revoke old, add new)
        //    var newRefresh = _tokenService.CreateRefreshToken(userId, daysValid: 7);
        //    await _refreshRepo.RevokeAsync(stored.Token, replacedBy: newRefresh.Token);
        //    await _refreshRepo.SaveAsync(newRefresh);

        //    // 4) create new access token using claims from principal
        //    // Note: recreate user object or claims; here we re-use claims to produce a new jwt
        //    var username = principal.Identity?.Name ?? principal.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Name)?.Value ?? "user";
        //    var roles = principal.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Role).Select(c => c.Value).ToList();

        //    var user = new ApplicationUser { IdUser = Convert.ToInt32 (userId), UserName = username, RoleId = roles. };
        //    var newAccessToken = _tokenService.CreateAccessToken(user);

        //    var resp = new TokenResponse
        //    {
        //        AccessToken = newAccessToken,
        //        RefreshToken = newRefresh.Token,
        //        ExpiresAt = DateTime.UtcNow.AddMinutes(15)
        //    };

        //    return Ok(resp);
        //}

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest req)
        {
            // basic validation aquí
            var created = await _userService.CreateUserAsync(req.Username, req.LastName, req.Password, req.RoleId);
            return CreatedAtAction(nameof(Register), new { id = created.IdUser }, new { created.IdUser });
        }

        [HttpPost("revoke")]
        public async Task<IActionResult> Revoke([FromBody] RefreshRequest req)
        {
            var stored = await _refreshRepo.GetByTokenAsync(req.RefreshToken);
            if (stored == null) return NotFound();

            await _refreshRepo.RevokeAsync(stored.Token);
            return NoContent();
        }

        // PUT api/<AuthController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // Mapear id_rol a roles (ejemplo simple)
        private System.Collections.Generic.List<string> MapRolesFromRoleId(int roleId)
        {
            return roleId switch
            {
                1 => new System.Collections.Generic.List<string> { "Admin" },
                2 => new System.Collections.Generic.List<string> { "Broker" },
                3 => new System.Collections.Generic.List<string> { "Cliente" }
            };
        }

        // DTO para register
        public class RegisterRequest
        {
            public string Username { get; set; } = null!;
            public string LastName { get; set; } = null!;
            public string Password { get; set; } = null!;
            public int RoleId { get; set; }
        }
    }
}
