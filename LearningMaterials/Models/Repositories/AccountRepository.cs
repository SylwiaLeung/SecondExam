using LearningMaterials.Data;
using LearningMaterials.Entities;
using LearningMaterials.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LearningMaterials.Models
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MaterialsDbContext _context;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly JwtDetails _jwtDetails;

        public AccountRepository(MaterialsDbContext context, IPasswordHasher<ApplicationUser> passwordHasher, JwtDetails jwtDetails)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _jwtDetails = jwtDetails;
        }
        public async Task<string> GenerateJwt(LoginDto dto)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user is null)
                throw new BadRequestException("Wrong Email or Password");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if (result == PasswordVerificationResult.Failed)
                throw new BadRequestException("Wrong Email or Password");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtDetails.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_jwtDetails.JwtExpireDays);

            var token = new JwtSecurityToken(_jwtDetails.JwtIssuer,
                _jwtDetails.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }

        public async Task RegisterUser(RegisterUserDto dto)
        {
            var newUser = new ApplicationUser()
            {
                Email = dto.Email,
                Name = dto.Name,
                RoleId = dto.RoleId
            };
            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = hashedPassword;

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
        }
    }
}