using Feedback360.Application.Features.Categories.Commands.CreateCategory;
using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.RolePermission.Command.AddPermission
{
    public class AddRolePermissionCommand : IRequest<Response<bool>>
    {
        public int RoleId { get; set; }

        public List<int> PermissionId { get; set; }
       

    }
}
