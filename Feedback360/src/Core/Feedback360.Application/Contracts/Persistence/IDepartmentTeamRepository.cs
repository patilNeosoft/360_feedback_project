using Feedback360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Contracts.Persistence
{
    public interface IDepartmentTeamRepository: IAsyncRepository<DepartmentTeam>
    {
        public Task<int> AddUserToTeam(DepartmentTeam departmentTeam);
        public Task<DepartmentTeam> FindTeamMember(int depTeamId);
        public Task<int> RemoveTeamMember(DepartmentTeam departmentTeam);
        public Task<List<EmployeeListInDepVm>> GetEmployeeListInDepartmemt(int bankId,int UserId);
        public Task<List<GroupMemberListVm>> GetTeamMembers(int userId);
        public Task<IEnumerable<SecondaryRole>> ListAllSecondaryRoles();
        public Task<TLDetailsVm> GetTeamLeaderDetails(int TeamLeadId);
        public Task<FinancialYear> GetCurrentFinancialYear();
        public Task<bool> DropGroup(int teamleadId);


    }
}
