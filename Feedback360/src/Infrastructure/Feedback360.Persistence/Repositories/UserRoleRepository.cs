
using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.UserRoles;
using Feedback360.Application.Responses;
using Feedback360.Domain.Entities;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Persistence.Repositories
{
    public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {
        public IMapper _mapper;
        protected readonly ApplicationDbContext _dbContext;
        public UserRoleRepository(IMapper mapper,ApplicationDbContext dbContext, ILogger<UserRole> logger) : base(dbContext, logger)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<UserRole> FindUserRoleById(int id)
        {
            UserRole userRole = await _dbContext.UserRoles.Where(u => u.RoleId == id).FirstOrDefaultAsync();
            return userRole;
        }
        public async Task RemoveUserRoleAsync(UserRole role)
        {

            role.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
        }
    }
}
