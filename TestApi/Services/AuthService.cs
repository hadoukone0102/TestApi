using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TestApi.Data;
using TestApi.DTOs.Users;
using TestApi.Helpers;
using TestApi.Interfaces;
using TestApi.Models;

namespace TestApi.Services
{
    public class AuthService : IAuthService
    {
        // variables de la base de données
        private readonly AppDbContext _context;
        // variable de la configuration
        private readonly IConfiguration _configuration;

        // Constructor
        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Methode de register
        public async Task<string> Register(UserDto userDto)
        {
            // Verification de l'email
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == userDto.Email);
            if (user != null)
            {
                return "Email already exists";
            }
            // Hashage du mot de passe
            using var hmac = new HMACSHA512();
            var newUser = new User
            {
                Email = userDto.Email,
                PasswordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.PasswordHash))),
                PasswordSalt = hmac.Key
            };
            // Ajout de l'utilisateur
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return "Utilisateur créer avec succès";
        }
        // Methode de login
        public async Task<string> Login(UserDto userDto)
        {
            // Verification de l'email
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == userDto.Email);
            if (user == null)
            {
                return "Email non trouver";
            }
            // Verification du mot de passe
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.PasswordHash)));
            if (computedHash != user.PasswordHash)
            {
                return "Invalid password";
            }
            // Generation du token
            return GenerateJwtToken(user);
        }

        //
        private string GenerateJwtToken(User user)
        {
            var keyString = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(keyString))
            {
                throw new ArgumentNullException("Jwt:Key", "JWT key cannot be null or empty.");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public async Task<ApiResponse<string>> RegisterUser(UserDto userDto)
        {
            // Vérification si l'utilisateur existe déjà
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == userDto.Email);
            if (user != null)
            {
                return new ApiResponse<string>(false, "Email already exists", string.Empty);
            }

            // Hashage du mot de passe
            using var hmac = new HMACSHA512();
            var newUser = new User
            {
                Email = userDto.Email,
                PasswordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.PasswordHash))),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return new ApiResponse<string>(true, "Utilisateur créé avec succès", string.Empty);
        }

        public async Task<ApiResponse<string>> LoginUser(UserDto userDto)
        {
            // Vérification si l'utilisateur existe
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == userDto.Email);
            if (user == null)
            {
                return new ApiResponse<string>(false, "Email non trouvé", string.Empty);
            }

            // Vérification du mot de passe
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.PasswordHash)));
            if (computedHash != user.PasswordHash)
            {
                return new ApiResponse<string>(false, "Mot de passe invalide", string.Empty);
            }

            // Génération du token
            var token = GenerateJwtToken(user);
            return new ApiResponse<string>(true, "Connexion réussie", token);
        }

    }

}
