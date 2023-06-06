using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserManagement_DB;
using UserManagement_DB.Models;
using UserManagement_Repositories.Dtos;

namespace UserManagement_Repositories
{
    public interface IAuthRepository
    {
        string GenerateTokenString(LoginUserDto user);
        Task<bool> Login(LoginUserDto user);
        Task<User> Register(RegisterUserDto registerUser);
    }
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManagementContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;

        public AuthRepository(UserManagementContext context,UserManager<IdentityUser> userManager, IConfiguration config)
        {
            _context = context;
            _userManager = userManager;
            _config = config;
        }

       

        public async Task<User> Register(RegisterUserDto registerUser)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerUser.UserName,
                Email = registerUser.UserName
            };

            var newUser = new User
            {
                UserName = registerUser.UserName,
                Email = registerUser.UserName,
                Password = registerUser.Password,
                IsActive = registerUser.IsActive,
                
            };
            var result = await _userManager.CreateAsync(identityUser, registerUser.Password);
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return newUser;

        }

        public async Task<bool> Login(LoginUserDto user)
        {
            var identityUser = await _userManager.FindByEmailAsync(user.UserName);
            if (identityUser is null)
            {
                return false;
            }

            return await _userManager.CheckPasswordAsync(identityUser, user.Password);
        }

        public string GenerateTokenString(LoginUserDto user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.UserName),
                new Claim(ClaimTypes.Role,"Admin")
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));

            var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                issuer: _config.GetSection("Jwt:Issuer").Value,
                audience: _config.GetSection("Jwt:Audience").Value,
                signingCredentials: signingCred);

            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }
    }
}
