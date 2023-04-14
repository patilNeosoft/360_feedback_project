using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.UserQueries.Queries.GetAllCommentsByQueryId
{
    public class GetAllCommentsQueryHandler: IRequestHandler<GetAllCommentsByQueryIdQuery, IEnumerable<GetAllCommentsVm>>
    {
        private readonly IUserQueryRepository _userQueryRepository;
        public IMapper _mapper;
        public GetAllCommentsQueryHandler(IMapper mapper, IUserQueryRepository userQueryRepository)
        {
            _userQueryRepository = userQueryRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetAllCommentsVm>> Handle(GetAllCommentsByQueryIdQuery request, CancellationToken cancellationToken)
        {
            var commentsByQueryId = await _userQueryRepository.FindAllCommentsByQueryId(request.QueryId);
            IEnumerable<GetAllCommentsVm> getAllComments = _mapper.Map<IEnumerable<GetAllCommentsVm>>(commentsByQueryId);
            return getAllComments;
        }
    }
    
}
