using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.UserRoles.Commands.DeleteUserRole
{
    public class DeleteUserRoleCommand:IRequest<Response<DeleteUserRoleDto>>
    {
        public int RoleId { get; set; }
    }
}
