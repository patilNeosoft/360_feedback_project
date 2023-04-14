using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.UserQueries.Command.DeleteUserQuery
{
    public class DeleteUserQueryCommandHandler: IRequestHandler<DeleteUserQueryCommand, bool>
    {
        private readonly IUserQueryRepository _userQueryRepository;
        public IMapper _mapper;
        public DeleteUserQueryCommandHandler(IMapper mapper, IUserQueryRepository userQueryRepository)
        {
            _userQueryRepository = userQueryRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteUserQueryCommand request, CancellationToken cancellationToken)
        {
            int queryIdToDelete = request.QueryId;
            var queryToDelete = await _userQueryRepository.FindQueryByQueryId(queryIdToDelete);
            if (queryToDelete == null)
            {
                throw new NotFoundException(nameof(queryToDelete), queryIdToDelete);

            }
            else
            {
                bool result = await _userQueryRepository.RemoveUserQueryAsync(queryToDelete);
                return (result);
            }

        }
    } 
    
    
}
