using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.RolePermission.Command.UpdatePermission;
using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.AdminQuery.Command
{
    public class ResolveAdminQueryHandler : IRequestHandler<ResolveAdminQueryCommand, Response<bool>>
    {
        private readonly IQueryRepository _queryRepository;
        public IMapper _mapper;
        public ResolveAdminQueryHandler(IMapper mapper, IQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
            _mapper = mapper;
        }
        public async Task<Response<bool>> Handle(ResolveAdminQueryCommand request, CancellationToken cancellationToken)
        {
            var status = await _queryRepository.ResolveQuery(request.QueryId,request.CommentDescription,request.RoleName);
 
            return new Response<bool>(status);
        }
    }
}
