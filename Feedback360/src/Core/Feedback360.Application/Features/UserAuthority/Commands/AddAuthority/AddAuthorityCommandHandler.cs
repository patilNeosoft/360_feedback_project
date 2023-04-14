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

namespace Feedback360.Application.Features.UserAuthority.Commands.AddAuthority
{
    public class AddAuthorityCommandHandler : IRequestHandler<AddAuthorityCommand, Response<bool>>
    {
        private readonly ILogger<AddAuthorityCommandHandler> _logger;
        private readonly IUserAuthorityRepository _userAuthorityRepository;
        private readonly IMapper _mapper;
        public AddAuthorityCommandHandler(ILogger<AddAuthorityCommandHandler> logger, IUserAuthorityRepository userAuthorityRepository, IMapper mapper)
        {
            _logger = logger;
            _userAuthorityRepository = userAuthorityRepository;
            _mapper = mapper;
        }

        public async Task<Response<bool>> Handle(AddAuthorityCommand request, CancellationToken cancellationToken)
        {
            bool isUserAuthorityAdded;
            _logger.LogInformation("Add Authority Handler is initiated");
            var userAuthorityToAdd = _mapper.Map<UserAuthorityMapping>(request);
            var userAuthorityAdded = _userAuthorityRepository.AddUserAuthority(userAuthorityToAdd);
            if(userAuthorityAdded != null)
            {
                isUserAuthorityAdded = true;           
            }
            else
            {
                isUserAuthorityAdded = false;         
            }
            return new Response<bool>(isUserAuthorityAdded);
        }
    }
}
