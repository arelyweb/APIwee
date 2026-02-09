using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using API_wee.Data;
using API_wee.Models;

namespace API_wee.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;
        private readonly PasswordHasher<ApplicationUser> _passwordHasher;

        public UserService(AppDbContext db)
        {
            _db = db;
            _passwordHasher = new PasswordHasher<ApplicationUser>();
        }

        // Autentica: busca por username y verifica el password contra el hash
        public async Task<ApplicationUser?> AuthenticateAsync(string username, string password)
        {
            var user = await _db.User.FirstOrDefaultAsync(n=> n.UserName == username);
            if (user == null) return null;

            var verifyResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (verifyResult == PasswordVerificationResult.Success ||
                verifyResult == PasswordVerificationResult.SuccessRehashNeeded)
            {
                // Opcional: si SuccessRehashNeeded -> re-hashear con el nuevo esquema y guardar
                if (verifyResult == PasswordVerificationResult.SuccessRehashNeeded)
                {
                    user.PasswordHash = _passwordHasher.HashPassword(user, password);
                    _db.User.Update(user);
                    await _db.SaveChangesAsync();
                }

                return user;
            }

            return null;
        }

        // Crea usuario: genera hash y guarda
        public async Task<ApplicationUser> CreateUserAsync(string username, string lastname, string password, short roleId)
        {
            var user = new ApplicationUser
            {
                UserName = username,
                LastName = lastname,
                RoleId = roleId
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, password);

            _db.User.Add(user);
            await _db.SaveChangesAsync();

            return user;
        }

        public async Task<ApplicationUser?> GetByIdAsync(int id)
        {
            return await _db.User.FindAsync(id);
        }
    }
}
