using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Persistence.Repositories
{
    public class DepartmentTeamRepository : BaseRepository<DepartmentTeam>, IDepartmentTeamRepository
    {
        public IMapper _mapper;
        protected readonly ApplicationDbContext _dbContext;
        public DepartmentTeamRepository(IMapper mapper, ApplicationDbContext dbContext, ILogger<DepartmentTeam> logger) : base(dbContext, logger)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<int> AddUserToTeam(DepartmentTeam departmentTeam)
        {
            DepartmentTeam teamlead = _dbContext.DepartmentTeams.Where(x => x.UserId == departmentTeam.TeamLeadId && x.IsDeleted == false).FirstOrDefault();
            if(teamlead == null)
            {
                DepartmentTeam departmentTeamLeader = new DepartmentTeam();
                departmentTeamLeader.TeamJoiningDate = DateTime.Now;
                departmentTeamLeader.UserId = departmentTeam.TeamLeadId;
                var leader = _dbContext.Users.Where(u => u.Id == departmentTeam.UserId).FirstOrDefault();
                departmentTeamLeader.DeptId = (int)leader.DeptId;
                SecondaryRole role = _dbContext.SecondaryRoles.Where(u=>u.SRoleName == "TeamLeader").FirstOrDefault();
                departmentTeamLeader.SRoleId = role.SRoleID;
                departmentTeamLeader.BankId = departmentTeam.BankId;
                departmentTeamLeader.TeamLeadId = departmentTeam.TeamLeadId;
                await _dbContext.AddAsync(departmentTeamLeader);
                await _dbContext.SaveChangesAsync();
            }
            departmentTeam.TeamJoiningDate = DateTime.Now;
            var user = _dbContext.Users.Where(u => u.Id == departmentTeam.UserId).FirstOrDefault();
            departmentTeam.DeptId = (int)user.DeptId;
            await _dbContext.AddAsync(departmentTeam);
            int result = await _dbContext.SaveChangesAsync();
            return result;
        }
        public async Task<DepartmentTeam> FindTeamMember(int depTeamId)
        {
            return await _dbContext.DepartmentTeams.Where(u => u.DeptTeamId == depTeamId).FirstOrDefaultAsync();
        }

        public async Task<int> RemoveTeamMember(DepartmentTeam departmentTeam)
        {
            departmentTeam.IsDeleted = true;
            int result = await _dbContext.SaveChangesAsync();
            return result;
        }

        //1.what user roles to be include
        public async Task<List<EmployeeListInDepVm>> GetEmployeeListInDepartmemt(int bankId, int userId)
        {
            User findUser = _dbContext.Users.Where(x => x.Id == userId).FirstOrDefault();
            List<User> availableDepartmentTeam = await _dbContext.Users.Where(u => u.DeptId == findUser.DeptId && u.BankId == bankId).Include(x => x.UserRole).ToListAsync();
            List<DepartmentTeam> allEmployees = _dbContext.DepartmentTeams.Where(u => u.IsDeleted == false).ToList();
            List<EmployeeListInDepVm> employeeListInDepVmList = new List<EmployeeListInDepVm>();
            foreach (var user in availableDepartmentTeam)
            {
                EmployeeListInDepVm employeeListInDepVm = new EmployeeListInDepVm();
                employeeListInDepVm.Department = _dbContext.Departments.Where(u => u.DeptId == user.DeptId).FirstOrDefault().DeptName;
                employeeListInDepVm.UserRole = user.UserRole.RoleName;
                employeeListInDepVm.UserName = user.FirstName + " " + user.LastName;
                employeeListInDepVm.UserId = user.Id;
                employeeListInDepVm.Bank = _dbContext.Banks.Where(u => u.BankId == user.BankId).FirstOrDefault().BankName;
                employeeListInDepVmList.Add(employeeListInDepVm);
            }
            foreach(var item in allEmployees)
            {
                var obj = employeeListInDepVmList.Find(u => u.UserId == item.UserId);
                if (obj != null)
                {
                    employeeListInDepVmList.Remove(obj);
                }

            }
            //var Teamleader = employeeListInDepVmList.Where(u => u.UserId == userId).FirstOrDefault();
            //employeeListInDepVmList.Remove(Teamleader);
            return employeeListInDepVmList;
        }

        public async Task<List<GroupMemberListVm>> GetTeamMembers(int userId)
            {
            List<DepartmentTeam> departmentTeams = new List<DepartmentTeam>();
            DepartmentTeam departmentTeamsobj = new DepartmentTeam();

            departmentTeams = await _dbContext.DepartmentTeams.Where(u => u.TeamLeadId == userId && u.IsDeleted == false).Include(x => x.SecondaryRole).ToListAsync();
                List<GroupMemberListVm> groupMemberListVmList = new List<GroupMemberListVm>();
            //get team if not a lead    
            if(departmentTeams.Count == 0)
            {
              departmentTeamsobj = await _dbContext.DepartmentTeams.Where(u => u.UserId == userId && u.IsDeleted == false).FirstOrDefaultAsync();
                if (departmentTeamsobj != null)
                {
                    departmentTeams = await _dbContext.DepartmentTeams.Where(u => u.TeamLeadId == departmentTeamsobj.TeamLeadId && u.IsDeleted == false).Include(x => x.SecondaryRole).ToListAsync();
                }
                }
            if (departmentTeams.Count != 0)
            {
                foreach (var departmentTeam in departmentTeams)
                {
                    GroupMemberListVm groupMemberListVm = new GroupMemberListVm();
                    groupMemberListVm.DeptTeamId = departmentTeam.DeptTeamId;
                    groupMemberListVm.TeamJoiningDate = departmentTeam.TeamJoiningDate;
                    groupMemberListVm.UserId = departmentTeam.UserId;
                    var user = _dbContext.Users.Where(x => x.Id == departmentTeam.UserId).FirstOrDefault();
                    groupMemberListVm.UserName = user.FirstName + " " + user.LastName;
                    groupMemberListVm.SRole = departmentTeam.SecondaryRole.SRoleName;
                    groupMemberListVmList.Add(groupMemberListVm);
                }
            }
                return groupMemberListVmList;
            }

        public async Task<IEnumerable<SecondaryRole>> ListAllSecondaryRoles()
            {
                return await _dbContext.SecondaryRoles.Where(x=>x.SRoleName != "TeamLeader").ToListAsync();
            }

        public async Task<TLDetailsVm> GetTeamLeaderDetails(int userId)
        {
            DepartmentTeam self = _dbContext.DepartmentTeams.Where(x => x.UserId == userId && x.IsDeleted == false).FirstOrDefault();
            TLDetailsVm vm = new TLDetailsVm();
            if (self != null)
            {
                User user = await _dbContext.Users.Where(x => x.Id == self.TeamLeadId).FirstOrDefaultAsync();
                vm.ContactNumber = user.ContactNumber;
                vm.FirstName = user.FirstName;
                vm.LastName = user.LastName;
                vm.Id = user.Id;
                vm.RoleId = user.RoleId;
                vm.BankId = user.BankId;
                vm.TeamJoiningDate = (DateTime)self.TeamJoiningDate;
                vm.EmployeeId = user.EmployeeId;
            }
            return vm;
        }

        public async Task<FinancialYear> GetCurrentFinancialYear()
        {
            FinancialYear fYear = await _dbContext.FinancialYears.Where(u => u.IsActive == true && u.IsDeleted == false).FirstOrDefaultAsync();
            return fYear;
        }

        public async Task<bool> DropGroup(int teamleadId)
        {
            List<DepartmentTeam> team = await _dbContext.DepartmentTeams.Where(x => x.TeamLeadId == teamleadId).ToListAsync();
            foreach(var item in team)
            {
                item.IsDeleted = true;
                await _dbContext.SaveChangesAsync();
            }
            return true;
        }
    }
}
