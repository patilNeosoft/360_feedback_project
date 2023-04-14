using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.AdminQuery.Command.DeleteCommand
{
    public class DeleteQueriesHandler : IRequestHandler<DeleteQueriesCommand, Response<bool>>
    {
        private readonly IQueryRepository _queryRepository;
        public IMapper _mapper;
        public DeleteQueriesHandler(IMapper mapper, IQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
            _mapper = mapper;
        }
        public async Task<Response<bool>> Handle(DeleteQueriesCommand request, CancellationToken cancellationToken)
        {
            var result = await _queryRepository.DeleteQueries(request.QueryId);
            return new Response<bool>(result);
        }
    }
}
