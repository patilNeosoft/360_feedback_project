using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.SelfFeedBack.Query.GetUserAuthorityDataByUserId
{
    public class GetUserAuthorityDataByUserIdQueryHandler: IRequestHandler<GetUserAuthorityDataByUserIdQuery, GetUserAuthorityDataByUserIdQueryVM>
    {
        private readonly ISelfFeedbackRepository _selfFeedbackRepository;
        public IMapper _mapper;
        public GetUserAuthorityDataByUserIdQueryHandler(IMapper mapper, ISelfFeedbackRepository selfFeedbackRepository)
        {
            _selfFeedbackRepository = selfFeedbackRepository;
            _mapper = mapper;
        }
        public async Task<GetUserAuthorityDataByUserIdQueryVM> Handle(GetUserAuthorityDataByUserIdQuery request, CancellationToken cancellationToken)
        {
            int id = request.UserId;
            var userAuthorityData = await _selfFeedbackRepository.FindAuthorityDataByUserId(id);
            var mapobject = _mapper.Map<GetUserAuthorityDataByUserIdQueryVM>(userAuthorityData);
            return (mapobject);

        }
    }
}
