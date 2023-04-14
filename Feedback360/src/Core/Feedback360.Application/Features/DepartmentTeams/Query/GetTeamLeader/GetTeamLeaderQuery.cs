using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.DepartmentTeams.Query.GetTeamLeader
{
    public class GetTeamLeaderQuery: IRequest<GetTeamLeaderQueryVm>
    {
        public int TeamLeadId { get; set; }
    }
}
