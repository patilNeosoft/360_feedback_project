using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.UserQueries.Command.CreateUserQuery
{
    public class CreateUserQueryCommand: IRequest<Response<CreateUserQueryDto>>
    {
       
        public string QueryTitle { get; set; }
        public string Description { get; set; }
        public bool QueryStatus { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int UserId { get; set; }
    }
}
