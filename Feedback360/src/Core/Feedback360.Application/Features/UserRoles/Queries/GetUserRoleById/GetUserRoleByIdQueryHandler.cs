using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.UserRoles.Queries.GetUserRoleById
{
    public class GetUserRoleByIdQueryHandler : IRequestHandler<GetUserRoleByIdQuery, GetUserRoleByIdVm>
    {
        private readonly IUserRoleRepository _userRoleRepository;
        public IMapper _mapper;
        public GetUserRoleByIdQueryHandler(IMapper mapper, IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
        }
        public async Task<GetUserRoleByIdVm> Handle(GetUserRoleByIdQuery request, CancellationToken cancellationToken)
        {
            int id = request.RoleId;
            var UserRole = await _userRoleRepository.FindUserRoleById(id);
            var mapobject=_mapper.Map<GetUserRoleByIdVm>(UserRole);
            return(mapobject);

        }
    }
}
