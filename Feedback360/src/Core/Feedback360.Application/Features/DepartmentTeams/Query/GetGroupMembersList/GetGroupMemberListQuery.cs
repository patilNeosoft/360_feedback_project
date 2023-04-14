using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.DepartmentTeams.Query.GetGroupMembersList
{
    public class GetGroupMemberListQuery: IRequest<IEnumerable<GetGroupMemberListVm>>
    {
        public int TeamLeadUserId { get; set; }
    }
}
