using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.UserQueries.Queries.GetAllCommentsByQueryId
{
    public class GetAllCommentsByQueryIdQuery: IRequest<IEnumerable<GetAllCommentsVm>>
    {
        public int QueryId { get; set; }
    
    }
}
