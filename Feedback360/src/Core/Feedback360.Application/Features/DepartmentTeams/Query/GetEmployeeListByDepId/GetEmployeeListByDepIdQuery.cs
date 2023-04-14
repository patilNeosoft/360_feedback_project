using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.DepartmentTeams.Query.GetEmployeeListByDepId
{
    public class GetEmployeeListByDepIdQuery:IRequest<IEnumerable<GetEmployeeListByDepIdVm>>
    {
        public int BankId { get; set; }
        public int UserId { get; set; }
    }
}
