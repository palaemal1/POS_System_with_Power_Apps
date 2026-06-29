using BAL.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model;
using Model.DTO;
using Model.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace BAL.Service
{
    public class AuthService(DataContent content,IConfiguration configuration):IAuthService
    {
       
        public async Task<TokenResponseDTO?> RefreshTokenAsync(RefreshTokenRequestDTO input)
        {
            var emp = await ValidateRefreshTokenAsync(input.employeeId, input.refreshToken);
            if (emp is null)
            {
                return null;
            }
            return await CreateTokenResponse(emp);
        }
        public async Task<TokenResponseDTO?> Login(AddNewEmployeeDTO input)
        {
            var emp = await content.Employees.FirstOrDefaultAsync(e => e.Email == input.email);
           
            if (emp is null)
            {
                return null;
            }
            if (new PasswordHasher<Employees>().VerifyHashedPassword(emp, emp.Password, input.password) == PasswordVerificationResult.Failed)
            {
                return null;
            }

           
            return await CreateTokenResponse(emp);
           
            ;
           
        }

        private async Task<TokenResponseDTO> CreateTokenResponse(Employees? emp)
        {
            return new TokenResponseDTO()
            {
                AccessToken = CreateToken(emp),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(emp)
            };

        }

       // private string CreateToken(Employees emp)
        private string CreateToken(Employees emp)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,emp.Email), 
                new Claim(ClaimTypes.NameIdentifier,emp.EmployeeId.ToString()), 
                new Claim(ClaimTypes.Role,emp.Role)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AppSettings:Token"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration["AppSettings:Issuer"],
                audience: configuration["AppSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        
        private async Task<Employees?> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
        {
            var emp = await content.Employees.FindAsync(userId);
            if (emp is null || emp.RefreshToken != refreshToken || emp.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return null;
            }
            return emp;
        }


        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private async Task<string> GenerateAndSaveRefreshTokenAsync(Employees emp)
        {
            var refreshToken = GenerateRefreshToken();
            emp.RefreshToken = refreshToken;
            emp.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await content.SaveChangesAsync();
            return refreshToken;
        }

    }
}
