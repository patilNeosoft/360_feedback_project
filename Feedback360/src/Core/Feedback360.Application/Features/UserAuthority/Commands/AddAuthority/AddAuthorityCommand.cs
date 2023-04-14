using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.UserAuthority.Commands.AddAuthority
{
    public class AddAuthorityCommand : IRequest<Response<bool>>
    {
        public int UserId { get; set; }
        public int? ReportingAuthority { get; set; }
        public int? ReviewingAuthority { get; set; }
    }
}
