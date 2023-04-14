using AutoMapper;
using Azure;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Domain.Entities;
using Feedback360.Domain.Models;
using Feedback360.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using NuGet.Protocol.Core.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Persistence.Repositories
{
    public class UserPermissionRepository : BaseRepository<RolePermissionMapping>, IUserPermissionRepository
    {
     
        protected readonly ApplicationDbContext _dbContext;
        public UserPermissionRepository(ApplicationDbContext dbContext, ILogger<RolePermissionMapping> logger) : base(dbContext, logger)
        {
         
            _dbContext = dbContext;
        }

        public async Task<bool> AddRolePermission(List<RolePermissionMapping> mappinglist)
        {
          
            try
            {
               
                foreach(var mapping in mappinglist)
                {
                   var status= GetpermissionRedundant(mapping.RoleId, mapping.PermissionId);
                    if (status == false) { await _dbContext.RolePermissionMappings.AddAsync(mapping); }
                    
                }
               _dbContext.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

          
        }

        public bool GetpermissionRedundant(int Roleid,int permissionid)
        {
           var status=  _dbContext.RolePermissionMappings.Where(x => x.RoleId == Roleid && x.PermissionId == permissionid).FirstOrDefault();
            if (status == null)
            {
                return false;
            }
            return true;

        }

       public async Task<List<RolePermission>> GetAllPermissions()
        {
          List<RolePermission> rolePermissions= await _dbContext.RolePermissions.ToListAsync();
            return rolePermissions;
        }

        public async Task<GetPermissionByRoleDto> GetPermissionByRole(int Roleid)
        {
            GetPermissionByRoleDto getPermissionByRoleDto = new GetPermissionByRoleDto();
            List<int> permissionDesc = new List<int>();
      
        List<RolePermissionMapping> permissionList  = await _dbContext.RolePermissionMappings.Where(x => x.RoleId == Roleid).Include(x => x.RolePermission).ToListAsync();
      
            foreach (var permission in permissionList)
            {
                permissionDesc.Add(permission.RolePermission.PermissionId);

            }
           return new GetPermissionByRoleDto { RoleId = Roleid, PermissionName = permissionDesc };
            
        }


        public async Task<bool> EditPermissions(GetPermissionByRoleDto getPermissionByRoleDto,List<int> permissionName)
        {
          
            foreach(var item in permissionName)
            {
              var data= await _dbContext.RolePermissionMappings.Where(x => x.RoleId == getPermissionByRoleDto.RoleId && x.PermissionId == item).FirstOrDefaultAsync();
               _dbContext.RolePermissionMappings.Remove(data);
                await _dbContext.SaveChangesAsync();

            };

            return true;


        }

    }
}
