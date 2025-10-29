using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Ms_User.DTOs;
using Ms_User.Models;

namespace Ms_User.Services
{
    public class UserService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly string key;
        public UserService(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            key = _configuration["Jwt:Key"];
        }
        public async Task<(bool Success, List<string> Errors)> CreateUser(LoginDTO loginDTO)
        {
            var existing = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (existing != null)
            {
                throw new InvalidOperationException("Usuário já existe");
            }
            var user = new ApplicationUser
            {
                Email = loginDTO.Email,
                UserName = loginDTO.Email,
            };
            var result = await _userManager.CreateAsync(user, loginDTO.Password);
            if (!result.Succeeded)
            {
                return (false, result.Errors.Select(e => e.Description).ToList());
            }
            return (true, new List<string>());
        }
        public async Task<string> UserLogin(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            var userId = user.Id;
            if (user == null)
            {
                throw new InvalidOperationException("Usuário não encontrado");
            }
            var isValid = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
            if (!isValid)
            {
                throw new InvalidOperationException("Senha incorreta");
            }
            var token = GenerateToken(loginDTO, userId);
            return token;
        }
        public string GenerateToken(LoginDTO loginDTO, string userId)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new InvalidOperationException("A chave JWT não está configurada.");
            }
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, loginDTO.Email)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}