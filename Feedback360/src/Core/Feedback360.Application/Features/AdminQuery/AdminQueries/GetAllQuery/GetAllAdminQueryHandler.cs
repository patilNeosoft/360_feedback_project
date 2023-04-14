using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.Banners.Queries.GetAllBanners;
using Feedback360.Application.Features.RolePermission.Query.GetAllPermissions;
using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.AdminQuery.AdminQueries.GetAllQuery
{
    public class GetAllAdminQueryHandler : IRequestHandler<GetAllAdminQueryCommand, Response<IEnumerable<GetAllAdminQueryVM>>>
    {
       private readonly IQueryRepository _queryRepository;
        public IMapper _mapper;
        public GetAllAdminQueryHandler(IMapper mapper,IQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<GetAllAdminQueryVM>>> Handle(GetAllAdminQueryCommand request, CancellationToken cancellationToken)
        {
            var res = await _queryRepository.GetAllAdminQuery();
            var queryList = _mapper.Map<List<GetAllAdminQueryVM>>(res);
            var response = new Response<IEnumerable<GetAllAdminQueryVM>>(queryList);
            return response;
        }

      
    }
}
