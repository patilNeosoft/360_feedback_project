using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.RolePermission.Query.GetAllPermissions
{
    public class GetAllPermissionsQueryHandler : IRequestHandler<GetAllPermissionsQuery, Response<IEnumerable<GetAllPermissionsVM>>>
    {
        private readonly IUserPermissionRepository _userPermissionRepository;
        private readonly IMapper _mapper;
        public GetAllPermissionsQueryHandler(IUserPermissionRepository userPermissionRepository, IMapper mapper)
        {
            _userPermissionRepository = userPermissionRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<GetAllPermissionsVM>>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
        {
            var res = await _userPermissionRepository.GetAllPermissions();
            var permissionList = _mapper.Map<List<GetAllPermissionsVM>>(res);
            var response = new Response<IEnumerable<GetAllPermissionsVM>>(permissionList);
            return response;

        }
    }
}
