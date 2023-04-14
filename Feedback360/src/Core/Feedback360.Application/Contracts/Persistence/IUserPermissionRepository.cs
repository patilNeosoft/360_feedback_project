using Feedback360.Domain.Entities;
using Feedback360.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Feedback360.Application.Contracts.Persistence
{
    public interface IUserPermissionRepository : IAsyncRepository<RolePermissionMapping>
    {
        public Task<bool> AddRolePermission(List<RolePermissionMapping> mappinglist);
        public Task<List<RolePermission>> GetAllPermissions();

        public Task<GetPermissionByRoleDto> GetPermissionByRole(int Roleid);

        public Task<bool> EditPermissions(GetPermissionByRoleDto getPermissionByRoleDto, List<int> permissionName);
    }
}
