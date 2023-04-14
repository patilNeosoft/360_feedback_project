using Feedback360.Application.Contracts.Persistence;
using Feedback360.Domain.Entities;
using Feedback360.Infrastructure.EncryptDecrypt;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Persistence.Repositories
{
    public class AdminRepository : BaseRepository<User>, IAdminRepository
    {
        public AdminRepository(ApplicationDbContext dbContext, ILogger<User> logger) : base(dbContext, logger)
        {
        }
        
        public async Task<User> AdminLoginAsync(string email, string password)
        {
            password = EncryptionDecryption.EncryptString(password);
            var userExists = await _dbContext.Users.Where(u => u.Email == email && u.Password == password).Include(x=>x.UserRole).FirstOrDefaultAsync();
            if(userExists != null && userExists.UserRole.RoleName == "Administrator")    //Make sure to add userExists Code during merge
            {
                
                return userExists;
            }
            else
            {
                throw new Exception("User Does not Exist");
            }
        }
        public async Task<User> GetUserByIdAsync(string employeeId)
        {
            return await _dbContext.Users.Where(u => u.EmployeeId == employeeId).FirstOrDefaultAsync();
        }
      
    }
}
