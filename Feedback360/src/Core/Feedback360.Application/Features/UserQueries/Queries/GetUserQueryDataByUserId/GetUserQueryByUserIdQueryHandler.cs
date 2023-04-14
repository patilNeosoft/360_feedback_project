using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Responses;
using Feedback360.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.UserQueries.Queries.GetUserQueryDataByUserId
{
    public class GetUserQueryByUserIdQueryHandler : IRequestHandler<GetUserQueryByUserIdQuery, Response<IEnumerable<GetUserQueryByUserIdVm>>>
    {
        private readonly IUserQueryRepository _userQueryRepository;
        public IMapper _mapper;
        public GetUserQueryByUserIdQueryHandler(IMapper mapper, IUserQueryRepository userQueryRepository)
        {
            _userQueryRepository = userQueryRepository;
            _mapper = mapper;
        }
        public async Task<Response<IEnumerable<GetUserQueryByUserIdVm>>> Handle(GetUserQueryByUserIdQuery request, CancellationToken cancellationToken)
        {
            var allQueries = await _userQueryRepository.ListAllUserQueries();
            IEnumerable<GetUserQueryByUserIdVm> getAllQueriesVm = _mapper.Map<IEnumerable<GetUserQueryByUserIdVm>>(allQueries).Where(u => u.UserId == request.UserId);
            return new Response<IEnumerable<GetUserQueryByUserIdVm>>(getAllQueriesVm);
            }
        }

    }
