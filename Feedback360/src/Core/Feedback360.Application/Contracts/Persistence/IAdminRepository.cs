using Feedback360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Contracts.Persistence
{
    public interface IAdminRepository : IAsyncRepository<User>
    {
        Task<User> AdminLoginAsync(string email, string password);
        public Task<User> GetUserByIdAsync(string employeeId);


    }
}
