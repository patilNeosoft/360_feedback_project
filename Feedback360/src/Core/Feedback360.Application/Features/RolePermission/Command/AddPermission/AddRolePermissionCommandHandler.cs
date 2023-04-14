using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Responses;
using Feedback360.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.RolePermission.Command.AddPermission
{
    public class AddRolePermissionCommandHandler: IRequestHandler<AddRolePermissionCommand, Response<bool>>
    {
        private readonly ILogger<AddRolePermissionCommandHandler> _logger;
        private readonly IUserPermissionRepository _userPermissionRepository;
        private readonly IMapper _mapper;

        public AddRolePermissionCommandHandler(ILogger<AddRolePermissionCommandHandler> logger, IUserPermissionRepository userPermissionRepository, IMapper mapper)
        {
            _logger = logger;
            _userPermissionRepository = userPermissionRepository;
            _mapper = mapper;
                
        }

        public async Task<Response<bool>> Handle(AddRolePermissionCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("AddRole command handler is initiated");
            List<RolePermissionMapping> mappinglist = new List<RolePermissionMapping>();
          
            for (int i = 0; i < request.PermissionId.Count; i++)
            {
              mappinglist.Add(new RolePermissionMapping() { RoleId = request.RoleId, PermissionId = request.PermissionId[i] });          

            }
            _userPermissionRepository.AddRolePermission(mappinglist);
            return new Response<bool>(true);

        }
    }
}
