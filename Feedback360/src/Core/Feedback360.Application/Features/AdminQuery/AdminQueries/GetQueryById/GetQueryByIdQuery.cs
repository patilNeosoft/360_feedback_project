using Feedback360.Application.Features.RolePermission.Query.GetPermissionsByRole;
using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.AdminQuery.AdminQueries.GetQueryById
{
    public class GetQueryByIdQuery: IRequest<Response<GetQueryByIdVM>>
    {
        public int QueryId { get; set; }
    }
}
