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

namespace Feedback360.Application.Features.UserRoles.Queries.GetAllUserRoles
{
    public class GetAllUserRolesQueryHandler : IRequestHandler<GetAllUserRolesQuery, Response<IEnumerable<GetAllUserRolesVm>>>
    {
        private readonly IUserRoleRepository _userRoleRepository;
        public IMapper _mapper;
        public GetAllUserRolesQueryHandler(IMapper mapper, IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
        }
        public async Task<Response<IEnumerable<GetAllUserRolesVm>>> Handle(GetAllUserRolesQuery request, CancellationToken cancellationToken)
        {
            var allUserRoles = await _userRoleRepository.ListAllAsync();
            IEnumerable<GetAllUserRolesVm> getAllUserRolesVms = _mapper.Map<IEnumerable<GetAllUserRolesVm>>(allUserRoles).Where(u=> u.IsDeleted == false);
            return new Response<IEnumerable<GetAllUserRolesVm>>(getAllUserRolesVms);

        }
    }
}
