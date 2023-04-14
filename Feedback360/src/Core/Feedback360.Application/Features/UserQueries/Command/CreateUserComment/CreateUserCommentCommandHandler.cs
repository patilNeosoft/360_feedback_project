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

namespace Feedback360.Application.Features.UserQueries.Command.CreateUserComment
{
    public class CreateUserCommentCommandHandler : IRequestHandler<CreateUserCommentCommand,Response<bool>>
    {
        private readonly IUserQueryRepository _userQueryRepository;
        public IMapper _mapper;
        public CreateUserCommentCommandHandler(IMapper mapper, IUserQueryRepository userQueryRepository)
        {
            _userQueryRepository = userQueryRepository;
            _mapper = mapper;
        }

        public async Task<Response<bool>> Handle(CreateUserCommentCommand request, CancellationToken cancellationToken)
        {
            var userCommentToAdd = _mapper.Map<Comment>(request);

            bool userCommentAdded = await _userQueryRepository.AddComment(userCommentToAdd);
            return new Response<bool>(userCommentAdded);
            
        }
    }

}

