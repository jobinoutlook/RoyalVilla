using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RoyalVilla_API.Data;
using RoyalVilla_API.Models;
using RoyalVilla_API.Models.DTO;
using System.Security.Claims;

namespace RoyalVilla_API.Services
{
    public class AuthService : IAuthService
    {

        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly UserMapper _userMapper;

        public AuthService(ApplicationDbContext db, UserMapper userMapper, IConfiguration configuration)
        {
            _db = db;
            _userMapper = userMapper;
            _configuration = configuration;
        }
        public async Task<bool> IsEmailExistsAsync(string email)
        {
            return await _db.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO loginRequestDTO)
        {
            try
            {
                var user = await _db.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == loginRequestDTO.Email.ToLower());

                if (user == null || user.Password != loginRequestDTO.Password)
                {
                    return null; // User not found or password mismatch
                }

                //Generate  token
                var token = GenerateJwtToken(user);

                return new LoginResponseDTO
                {
                    UserDTO = _userMapper.ToUserDTO(user),
                    Token = token

                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while logging in: {ex.Message}", ex);
            }
        }

        public async Task<UserDTO?> RegisterAsync(RegisterationRequestDTO registerationRequestDTO)
        {
            try
            {
                if (await IsEmailExistsAsync(registerationRequestDTO.Email))
                {
                    //return null; // Email already exists
                    throw new InvalidOperationException($"User with email {registerationRequestDTO.Email} already exists.");
                }

                User user = new()
                {
                    Email = registerationRequestDTO.Email,
                    Name = registerationRequestDTO.Name,
                    Password = registerationRequestDTO.Password, // In a real application, hash the password before storing it
                    Role = string.IsNullOrEmpty(registerationRequestDTO.Role) ? "Customer" : registerationRequestDTO.Role,
                    CreatedDate = DateTime.Now
                };

                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();

                
                return _userMapper.ToUserDTO(user);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while registering the user: {ex.Message}", ex);
            }
        }

        private string GenerateJwtToken(User user)
        {
            // Implement token generation logic here (e.g., JWT)
            var key = _configuration["JwtSettings:SecretKey"]??"";
            var encodedKey = System.Text.Encoding.UTF8.GetBytes(key);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new System.Security.Claims.Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new System.Security.Claims.Claim(ClaimTypes.Email, user.Email),
                    new System.Security.Claims.Claim(ClaimTypes.Role, user.Role),
                    new System.Security.Claims.Claim(ClaimTypes.Name, user.Name)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(encodedKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
