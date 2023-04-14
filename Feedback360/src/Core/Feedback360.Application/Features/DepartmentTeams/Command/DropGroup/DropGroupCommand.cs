using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.DepartmentTeams.Command.DropGroup
{
    public class DropGroupCommand : IRequest<Response<bool>>
    {
        public int TeamLeadId { get; set; }
    }
}
