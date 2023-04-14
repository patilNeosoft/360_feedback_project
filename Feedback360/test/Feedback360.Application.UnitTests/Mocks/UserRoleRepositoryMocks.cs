using Feedback360.Application.Contracts.Persistence;
using Feedback360.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.UnitTests.Mocks
{
    public class UserRoleRepositoryMocks
    {
        public static Mock<IUserRoleRepository> UserRoleRepository()
        {
            var mockUserRoleRepository = new Mock<IUserRoleRepository>();
            
            var userRoles = new List<UserRole>
            {
                new UserRole
                {
                  RoleId =1,
                  RoleName = "Test",
                  IsActive = true,
                  IsDeleted = false
                } 
            };

           //add new user role
           mockUserRoleRepository.Setup(repo => repo.AddAsync(It.IsAny<UserRole>())).ReturnsAsync(
              
               (UserRole userRole) =>
                {
                    UserRole newRole = new UserRole();
                    newRole.RoleName = "Test1";
                    newRole.IsDeleted = false;
                    newRole.IsActive = true;
                    userRoles.Add(newRole);
                    return newRole;
                });

            //get all user roles
            mockUserRoleRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(userRoles);

            //get user role by Id
            mockUserRoleRepository.Setup(repo => repo.FindUserRoleById(It.IsAny<int>())).ReturnsAsync(
                (int RoleId) =>
                {
                    return userRoles.SingleOrDefault(x => x.RoleId == 1);
                });

            //delete user role
            mockUserRoleRepository.Setup(repo => repo.DeleteAsync(It.IsAny<UserRole>())).Callback(
                (UserRole UserRole) =>
                {
                    userRoles.Remove(UserRole);
                });

            //update user role
            mockUserRoleRepository.Setup(repo => repo.UpdateAsync(It.IsAny<UserRole>())).Callback(
                (UserRole role) =>
                {
                    var oldRole = userRoles.First(x => x.RoleId == role.RoleId);
                    var index = userRoles.IndexOf(oldRole);
                    if (index != -1)
                        userRoles[index] = oldRole;
                });
            return mockUserRoleRepository;
        }
    }
}
