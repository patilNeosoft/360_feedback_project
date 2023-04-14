using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.RolePermission.Query.GetAllPermissions
{
    public class GetAllPermissionsQuery: IRequest<Response<IEnumerable<GetAllPermissionsVM>>>
    {

    }
}
