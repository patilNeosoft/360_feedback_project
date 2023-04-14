using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.RolePermission.Query.GetAllPermissions;
using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.RolePermission.Query.GetPermissionsByRole
{
    public class GetPermissionByRoleQueryHandler: IRequestHandler<GetPermissionByRoleQuery, Response<GetPermissionByRoleQueryVM>>
    {
        private readonly IUserPermissionRepository _userPermissionRepository;
        private readonly IMapper _mapper;
        public GetPermissionByRoleQueryHandler(IUserPermissionRepository userPermissionRepository, IMapper mapper)
        {
            _userPermissionRepository = userPermissionRepository;
            _mapper = mapper;
        }

        public async Task<Response<GetPermissionByRoleQueryVM>> Handle(GetPermissionByRoleQuery request, CancellationToken cancellationToken)
        {
            var res = await _userPermissionRepository.GetPermissionByRole(request.RoleId);
            var permissionList = _mapper.Map<GetPermissionByRoleQueryVM>(res);
            var response = new Response<GetPermissionByRoleQueryVM>(permissionList);
            return response;


        }
    }
}
