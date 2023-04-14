using Feedback360.Application.Features.AdminQuery.AdminQueries.GetAllQuery;
using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.AdminQuery.AdminQueries.GetCommentsById
{
    public class GetCommentsByIdQuery: IRequest<IEnumerable<GetCommentsByIdQueryDto>>
    {
        public int QueryId { get; set; }
    }
}
