using Feedback360.Application.Features.UserRoles;
using Feedback360.Application.Responses;
using Feedback360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Feedback360.Application.Contracts.Persistence
{
    public interface IUserRoleRepository : IAsyncRepository<UserRole>
    {
        public Task<UserRole> FindUserRoleById(int id);
        public Task RemoveUserRoleAsync(UserRole role);
    }
}
