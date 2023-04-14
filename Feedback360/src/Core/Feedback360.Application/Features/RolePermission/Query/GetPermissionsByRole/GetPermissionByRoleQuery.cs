using Feedback360.Application.Features.RolePermission.Query.GetAllPermissions;
using Feedback360.Application.Responses;
using Feedback360.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.RolePermission.Query.GetPermissionsByRole
{
    public class GetPermissionByRoleQuery: IRequest<Response<GetPermissionByRoleQueryVM>>
    {
        public int RoleId { get; set; }
    }
}
