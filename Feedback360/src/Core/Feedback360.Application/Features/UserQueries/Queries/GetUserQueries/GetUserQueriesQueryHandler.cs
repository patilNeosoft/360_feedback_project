using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.UserQueries.Queries.GetUserQueries
{
    public class GetUserQueriesQueryHandler: IRequestHandler<GetUserQueriesQuery, Response<GetUserQueriesQueryVm>>
    {
        private readonly IUserQueryRepository _userQueryRepository;
        public IMapper _mapper;
        public GetUserQueriesQueryHandler(IMapper mapper, IUserQueryRepository userQueryRepository)
        {
            _userQueryRepository = userQueryRepository;
            _mapper = mapper;
        }
        public async Task<Response<GetUserQueriesQueryVm>> Handle(GetUserQueriesQuery request, CancellationToken cancellationToken)
        {
            var queryById = await _userQueryRepository.FindQueryByQueryId(request.QueryId);
            GetUserQueriesQueryVm getQuery = _mapper.Map<GetUserQueriesQueryVm>(queryById);
            return new Response<GetUserQueriesQueryVm>(getQuery);
        }
    }
   
}
