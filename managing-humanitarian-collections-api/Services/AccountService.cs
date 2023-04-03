using managing_humanitarian_collections_api.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using managing_humanitarian_collections_api.Models.UserModels;
using managing_humanitarian_collections_api.Exceptions;
using managing_humanitarian_collections_api.Models;

namespace managing_humanitarian_collections_api.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
        string GenerateJwt(LoginDto dto);
    }
    public class AccountService : IAccountService
    {
        private readonly ManagingCollectionsDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public AccountService(ManagingCollectionsDbContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }
        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                RoleId = dto.RoleId
            };
            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);

            newUser.HashPassword = hashedPassword;
            _context.Users.Add(newUser);
            _context.SaveChanges();

            var user = _context.Users
                .FirstOrDefault(u => u.Email == dto.Email);
            var newProfile = new Profile()
            {
                User = user,
                FirstName = dto.Email,
                LastName = null,
                Avatar = null,
                ContactNumber = null,
                Nip = null,
                Regon = null,
                Name = dto.Email,
            };
            _context.Profiles.Add(newProfile);
            _context.Attach(user);
            _context.SaveChanges();


        }
        public string GenerateJwt(LoginDto dto)
        {
            var user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == dto.Email);

            if (user is null)
            {
                throw new BadRequestException("niepoprawny login lub hasło");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.HashPassword, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("niepoprawny login lub hasło");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.Email}"),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
                new Claim("id", user.Id.ToString()),
                new Claim("role", user.Role.Name),
                new Claim("email", user.Email)

            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                   _authenticationSettings.JwtIssuer,
                   claims,
                   expires: expires,
                   signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        
    }
}
