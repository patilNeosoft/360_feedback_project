using Feedback360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Contracts.Persistence
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        public Task<AuthResponse> UserLogin(string employeeid, string password , int bankid);
        public Task<User> UserExists(string email);
        public Task<User> ResetUserPassword(ResetPassword resetPassword);
        public Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword);
    }
}
