using Feedback360.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.SelfFeedBack.Query.GetFeedBackByUserId
{
    public class GetFeedBackByUserIdQuery: IRequest<List<GetFeedBackByUserIdQueryVm>>
    {
        public int UserId { get; set; }
    }
}
