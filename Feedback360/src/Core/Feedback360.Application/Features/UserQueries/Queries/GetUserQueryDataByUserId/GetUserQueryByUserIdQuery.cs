using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.UserQueries.Queries.GetUserQueryDataByUserId
{
    public class GetUserQueryByUserIdQuery: IRequest<Response<IEnumerable<GetUserQueryByUserIdVm>>>
    {
        public int UserId { get; set; }
        
    }
}
