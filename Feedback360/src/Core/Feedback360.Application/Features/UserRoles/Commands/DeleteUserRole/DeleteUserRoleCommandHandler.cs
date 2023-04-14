using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Exceptions;
using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.UserRoles.Commands.DeleteUserRole
{
    public class DeleteUserRoleCommandHandler:IRequestHandler<DeleteUserRoleCommand,Response<DeleteUserRoleDto>>
    {
        private readonly IUserRoleRepository _userRoleRepository;
        public IMapper _mapper;
        

        
        public DeleteUserRoleCommandHandler(IMapper mapper, IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
        }
       
        public async Task<Response<DeleteUserRoleDto>> Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
        {
            int roleIdToDelete = request.RoleId;
            var userRoleToDelete = await _userRoleRepository.FindUserRoleById(roleIdToDelete);
            if (userRoleToDelete == null)
            {
                throw new NotFoundException(nameof(userRoleToDelete), roleIdToDelete);

            }
            else
            {
                await _userRoleRepository.RemoveUserRoleAsync(userRoleToDelete);
                DeleteUserRoleDto deletedUserRoleName = _mapper.Map<DeleteUserRoleDto>(userRoleToDelete);
                return new Response<DeleteUserRoleDto>(deletedUserRoleName);
            }
           
        }
    } 
}
