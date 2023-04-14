using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.AdminQuery.AdminQueries.GetAllQuery;
using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.AdminQuery.AdminQueries.GetCommentsById
{
    public class GetCommentsByIdQueryHandler: IRequestHandler<GetCommentsByIdQuery,IEnumerable<GetCommentsByIdQueryDto>>
    {
        private readonly IQueryRepository _queryRepository;
        public IMapper _mapper;
        public GetCommentsByIdQueryHandler(IMapper mapper, IQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
            _mapper = mapper;
        }



        public async Task<IEnumerable<GetCommentsByIdQueryDto>> Handle(GetCommentsByIdQuery request, CancellationToken cancellationToken)
        {
            var res = await _queryRepository.GetAllCommentsByQueryId(request.QueryId);
            var commentList = _mapper.Map<List<GetCommentsByIdQueryDto>>(res);
            var response = (commentList);
            return response;
        }

    }
}
