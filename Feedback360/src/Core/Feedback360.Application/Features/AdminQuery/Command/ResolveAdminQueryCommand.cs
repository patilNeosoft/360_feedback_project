using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.AdminQuery.Command
{
    public class ResolveAdminQueryCommand : IRequest<Response<bool>>
    {
        public int QueryId { get; set; }
        public string CommentDescription { get; set; }
        public string  RoleName { get; set; }
    }
}
