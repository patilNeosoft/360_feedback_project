using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.DepartmentTeams.Query.GetEmployeeListByDepId
{
    public class GetEmployeeListByDepIdVm
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public string Department { get; set; }
        public string Bank { get; set; }

    }
}
