using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.UserQueries.Command.CreateUserComment
{
    public class CreateUserCommentCommand: IRequest<Response<bool>>
    {
        public string CommentText { get; set; }
        public int QueryId { get; set; }
        public string RoleName { get; set; }
    }
}
