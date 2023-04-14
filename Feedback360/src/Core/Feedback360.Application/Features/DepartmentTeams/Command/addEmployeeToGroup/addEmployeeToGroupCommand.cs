using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.DepartmentTeams.Command.addEmployeeToGroup
{
    public class addEmployeeToGroupCommand:IRequest<Response<bool>>
    {
      
        public int TeamLeadId { get; set; }
        public int UserId { get; set; }
        public int DeptId { get; set; }
        public int BankId { get; set; }
        public int SRoleId { get; set; }
    }
}
