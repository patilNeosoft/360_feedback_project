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

namespace Feedback360.Application.Features.UserRoles.Commands.UpdateUserRole
{
    public class UpdateUserRoleCommandHandler:IRequestHandler<UpdateUserRoleCommand,Response<UpdateUserRoleDto>>
    {
        private readonly IUserRoleRepository _userRoleRepository;
        public IMapper _mapper;
        public UpdateUserRoleCommandHandler(IMapper mapper, IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
        }

        public async Task<Response<UpdateUserRoleDto>> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            int roleId = request.RoleId;
            var userRoleFromDb =  await _userRoleRepository.FindUserRoleById(roleId);

            if (userRoleFromDb == null)
            {
                throw new NotFoundException(nameof(userRoleFromDb), request.RoleId);
            }
            else
            {
                var userRoleToUpdate = _mapper.Map(request, userRoleFromDb);

                await _userRoleRepository.UpdateAsync(userRoleToUpdate);

                var updatedUserRole = _mapper.Map<UpdateUserRoleDto>(userRoleToUpdate);

                return new Response<UpdateUserRoleDto>(updatedUserRole);
            }
        }
    }
}

