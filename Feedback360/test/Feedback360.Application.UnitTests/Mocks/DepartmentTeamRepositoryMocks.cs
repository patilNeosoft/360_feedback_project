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
    public class DepartmentTeamRepositoryMocks
    {
        public static Mock<IDepartmentTeamRepository> DepartmentTeamRepository()
        {
            var mockDepartmentTeamRepository = new Mock<IDepartmentTeamRepository>();

            var departmentTeam = new List<DepartmentTeam>
            {
                new DepartmentTeam
                {
             DeptTeamId = 1,
             TeamLeadId = 1,
             UserId = 1,
             DeptId = 1,
             BankId = 1,
             SRoleId = 1,
             TeamJoiningDate = DateTime.Now,
             IsDeleted = false
                },
                new DepartmentTeam
                {
             DeptTeamId = 2,
             TeamLeadId = 1,
             UserId = 2,
             DeptId = 1,
             BankId = 1,
             SRoleId = 1,
             TeamJoiningDate = DateTime.Now,
             IsDeleted = false
                }
            };

            var users = new List<User>
            {
                new User
                {
                    Id = 1,
                    RoleId = 1,
                    BankId = 1,
                    FirstName = "Abc",
                    LastName = "Xyz",
                    Email = "abc@gmail.com",
                    EmployeeId = "9566",
                    Organization = "Neo",
                    Password = "Abcd@123",
                    ContactNumber = "0987654321"
                }
            };
            var secondaryRoles = new List<SecondaryRole> {
                new SecondaryRole
                {
                    SRoleID =1,
                    SRoleName ="TeamLeader"
                },
                new SecondaryRole
                {
                    SRoleID =2,
                    SRoleName ="Role1"
                }
            };
            var financialYear = new List<FinancialYear>
            {
            new FinancialYear
                {
            Id = 1,
            StartYear = 2022,
            EndYear = 2023,
            StartDate = "2022-12-29",
            EndDate = "2023-12-29",
            IsActive = true
                }
            };

            //findteam members
            mockDepartmentTeamRepository.Setup(repo => repo.FindTeamMember(It.IsAny<int>())).ReturnsAsync(
                (int depTeamId) =>
                {
                    return departmentTeam.SingleOrDefault(x => x.DeptTeamId == 1);
                });

            //get all available members
            mockDepartmentTeamRepository.Setup(repo => repo.GetTeamLeaderDetails(It.IsAny<int>())).Returns(
               (int userId) =>
               {
                   return users.SingleOrDefault(x => x.Id == 1);
               });

            mockDepartmentTeamRepository.Setup(repo => repo.ListAllSecondaryRoles()).ReturnsAsync(secondaryRoles);

            mockDepartmentTeamRepository.Setup(repo => repo.GetEmployeeListInDepartmemt(It.IsAny<int>(), It.IsAny<int>())).Returns(
               (int bankId, int userId) =>
               {
                   User user = users.Where(x => x.Id == userId).FirstOrDefault();
                   return users.SingleOrDefault(x => x.BankId == user.BankId && x.DeptId == user.DeptId);
               });

            mockDepartmentTeamRepository.Setup(repo => repo.RemoveTeamMember(It.IsAny<Domain.Entities.DepartmentTeam>())).Callback(
                 (Domain.Entities.DepartmentTeam DepartmentTeam) =>
                 {
                     departmentTeam.Remove(DepartmentTeam);
                 });


            return mockDepartmentTeamRepository;

        }
    }
}
