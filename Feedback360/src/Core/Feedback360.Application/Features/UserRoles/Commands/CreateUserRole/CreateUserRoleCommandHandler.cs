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

namespace Feedback360.Application.Features.UserRoles.Commands.CreateUserRole
{
    public class CreateUserRoleCommandHandler : IRequestHandler<CreateUserRoleCommand, Response<bool>>
    {
        private readonly IUserRoleRepository _userRoleRepository;
        public IMapper _mapper;
        public CreateUserRoleCommandHandler(IMapper mapper, IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
        }

        public async Task<Response<bool>> Handle(CreateUserRoleCommand request, CancellationToken cancellationToken)
        {

            bool IsUserRoleAdded;
            var userRoleToAdd = _mapper.Map<UserRole>(request);
            var userRoleAdded = await _userRoleRepository.AddAsync(userRoleToAdd);
            if (userRoleAdded != null)
            {
                IsUserRoleAdded = true;

            }
            else
            {
                IsUserRoleAdded = false;
            }
            return new Response<bool>(IsUserRoleAdded);
        }
    }
}


