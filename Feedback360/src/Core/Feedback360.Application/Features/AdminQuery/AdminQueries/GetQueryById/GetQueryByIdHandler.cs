using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.RolePermission.Query.GetPermissionsByRole;
using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.AdminQuery.AdminQueries.GetQueryById
{
    public class GetQueryByIdHandler : IRequestHandler<GetQueryByIdQuery, Response<GetQueryByIdVM>>
    {
        private readonly IQueryRepository _queryRepository;
        public IMapper _mapper;
        public GetQueryByIdHandler(IMapper mapper, IQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
            _mapper = mapper;
        }
        public async Task<Response<GetQueryByIdVM>> Handle(GetQueryByIdQuery request, CancellationToken cancellationToken)
        {
            var res = await _queryRepository.GetQueryById(request.QueryId);
            var singleOject = _mapper.Map<GetQueryByIdVM>(res);
            var response = new Response<GetQueryByIdVM>(singleOject);
            return response;
        }
    }
}
