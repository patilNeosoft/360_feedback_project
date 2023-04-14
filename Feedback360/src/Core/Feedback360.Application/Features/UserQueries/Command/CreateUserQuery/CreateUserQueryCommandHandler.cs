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

namespace Feedback360.Application.Features.UserQueries.Command.CreateUserQuery
{
    public class CreateUserQueryCommandHandler: IRequestHandler<CreateUserQueryCommand, Response<CreateUserQueryDto>>
    {
        private readonly IUserQueryRepository _userQueryRepository;
        public IMapper _mapper;
        public CreateUserQueryCommandHandler(IMapper mapper, IUserQueryRepository userQueryRepository)
        {
            _userQueryRepository = userQueryRepository;
            _mapper = mapper;
        }

        public async Task<Response<CreateUserQueryDto>> Handle(CreateUserQueryCommand request, CancellationToken cancellationToken)
        {
            var userQueryToAdd = _mapper.Map<Query>(request);
            userQueryToAdd.QueryStatus = false;
            userQueryToAdd.IsDeleted = false;
            var userQueryAdded = await _userQueryRepository.AddAsync(userQueryToAdd);
            return new Response<CreateUserQueryDto>(_mapper.Map<CreateUserQueryDto>(userQueryAdded));
        }
    }

}
