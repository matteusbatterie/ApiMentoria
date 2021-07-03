using Core.Abstractions.Repositories;
using Core.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Core.Enums;

namespace Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;

        }

        public async Task<string> GenerateAuthenticationAsync(string userEmail)
        {
            try
            {
                var user = await _userRepository.GetUserByEmailAsync(userEmail);
                var tokenHandler = new JwtSecurityTokenHandler();
                var secretKey = Encoding.UTF8.GetBytes(_configuration["JwtData:JwtSecret"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = _configuration["JwtData:Issuer"],
                    Audience = _configuration["JwtData:Issuer"],
                    Subject = GenerateClaims(user.Id, user.Role),
                    Expires = DateTime.Now.AddHours(Convert.ToInt32(_configuration["JwtData:JwtExpirationHours"])),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> ValidateCredentialsAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);

            // if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            // {
            //     return true;
            // }
            if(user != null && user.Email == email && user.Password == password)
            {
                return true;
            }

            return false;
        }

        private ClaimsIdentity GenerateClaims(int userId, UserRole userRole)
        {
            return new ClaimsIdentity(new Claim[]
                  {
                    new Claim("userId", userId.ToString()),
                    new Claim("userRole", userRole.ToString())
                  });
        }
    }
}
