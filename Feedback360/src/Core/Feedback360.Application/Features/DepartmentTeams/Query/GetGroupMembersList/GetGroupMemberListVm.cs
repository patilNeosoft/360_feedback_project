using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.DepartmentTeams.Query.GetGroupMembersList
{
    public class GetGroupMemberListVm
    {
        public int DeptTeamId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string SRole { get; set; }
        public DateTime? TeamJoiningDate { get; set; }
    }
}
