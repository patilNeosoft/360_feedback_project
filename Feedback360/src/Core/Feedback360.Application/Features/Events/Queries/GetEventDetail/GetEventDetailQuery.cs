using Feedback360.Application.Responses;
using MediatR;
using System;

namespace Feedback360.Application.Features.Events.Queries.GetEventDetail
{
    public class GetEventDetailQuery: IRequest<Response<EventDetailVm>>
    {
        public string Id { get; set; }
    }
}
