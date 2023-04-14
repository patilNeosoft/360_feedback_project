using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.RolePermission.Command.AddPermission;
using Feedback360.Application.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.RolePermission.Command.UpdatePermission
{
    public class UpdatePermissionCommandHandler: IRequestHandler<UpdatePermissionCommand, Response<bool>>
    {
        private readonly ILogger<AddRolePermissionCommandHandler> _logger;
        private readonly IUserPermissionRepository _userPermissionRepository;
        private readonly IMapper _mapper;
        public UpdatePermissionCommandHandler(ILogger<AddRolePermissionCommandHandler> logger, IUserPermissionRepository userPermissionRepository, IMapper mapper)
        {
            _logger = logger;
            _userPermissionRepository = userPermissionRepository;
            _mapper = mapper;
        }
        public async Task<Response<bool>> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {
           var permissionObject= await _userPermissionRepository.GetPermissionByRole(request.RoleId);
            var status=await _userPermissionRepository.EditPermissions(permissionObject,request.PermissionName);
            return new Response<bool>(status) ;

        }
    }
}
