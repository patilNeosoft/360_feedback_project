using Feedback360.Application.Responses;
using MediatR;
using System.Collections.Generic;

namespace Feedback360.Application.Features.Events.Queries.GetEventsList
{
    public class GetEventsListQuery: IRequest<Response<IEnumerable<EventListVm>>>
    {

    }
}
