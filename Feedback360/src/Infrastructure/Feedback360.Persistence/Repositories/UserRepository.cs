using Feedback360.Application.Contracts;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Domain.Entities;
using Feedback360.Infrastructure.EncryptDecrypt;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
       private readonly ILoggedInUserService _loggedInUserService;
        public UserRepository(ApplicationDbContext dbContext, ILogger<User> logger, IConfiguration configuration, ILoggedInUserService loggedInUserService) : base(dbContext, logger)
        {
            _configuration = configuration;
            _logger = logger;
            _loggedInUserService = loggedInUserService;
        }

        public async Task<AuthResponse> UserLogin(string employeeid, string password,int bankid)
        {
            password=EncryptionDecryption.EncryptString(password);
            var res = _dbContext.Users.Where(x => x.EmployeeId == employeeid && x.Password == password && x.BankId==bankid).Include(x => x.UserRole).FirstOrDefault();
            AuthResponse authResponse = new AuthResponse();
            if (res == null)
            {
                _logger.LogInformation("Result is null");
            }
            var authclaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, employeeid),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Jti,res.Id.ToString()),
                new Claim(ClaimTypes.Role,res.UserRole.RoleName)
            };
            var authsigninkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Issuer"],
                expires: DateTime.Now.AddDays(2),
                claims: authclaims,
                signingCredentials: new SigningCredentials(authsigninkey, SecurityAlgorithms.HmacSha256)
                );
            var tokenwriter = new JwtSecurityTokenHandler().WriteToken(token);
            string userJwttokenhandler = JsonConvert.SerializeObject(new { Token = tokenwriter });
            authResponse.Token = userJwttokenhandler;
            authResponse.RoleName = res.UserRole.RoleName;
            authResponse.UserID = res.Id;
            authResponse.RoleId=res.RoleId;
            authResponse.UserName=res.FirstName;
            authResponse.BankId=res.BankId;
            authResponse.Email=res.Email; 
            return authResponse;
        }

        public async Task<User> UserExists(string email)
        {
            var userExists = await _dbContext.Users.Where(u=> u.Email == email).FirstOrDefaultAsync();
            if(userExists == null)
            {
                throw new Exception("User not found");
            }
            else
            {
                return userExists;
            }
        }
        public async Task<User> ResetUserPassword(ResetPassword resetPassword)
        {
            User userExists = await UserExists(resetPassword.Email);
            if(userExists!=null)
            {
                resetPassword.NewPassword = EncryptionDecryption.EncryptString(resetPassword.NewPassword);
                userExists.Password = resetPassword.NewPassword;
                await _dbContext.SaveChangesAsync();
                return userExists;
            }
            else
            {
                throw new Exception("Email does not exist in Database");
            }
        }
        public async Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword)
        {
            oldPassword = EncryptionDecryption.EncryptString(oldPassword);
            newPassword = EncryptionDecryption.EncryptString(newPassword);
            var userExists = await _dbContext.Users.Where(u => u.Id == userId && u.Password == oldPassword).FirstOrDefaultAsync();
            if (userExists != null)
            {
                userExists.Password = newPassword;
                _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Wrong Password");
            }
            return true;
        }
    }
}
