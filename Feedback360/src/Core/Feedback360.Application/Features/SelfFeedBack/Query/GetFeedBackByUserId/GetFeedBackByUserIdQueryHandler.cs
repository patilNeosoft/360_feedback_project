using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.SelfFeedBack.Query.GetFeedBackByUserId
{
    public class GetFeedBackByUserIdQueryHandler: IRequestHandler<GetFeedBackByUserIdQuery, List<GetFeedBackByUserIdQueryVm>>
    {
        private readonly ISelfFeedbackRepository _selfFeedbackRepository;
        public IMapper _mapper;
        public GetFeedBackByUserIdQueryHandler(IMapper mapper, ISelfFeedbackRepository selfFeedbackRepository)
        {
            _selfFeedbackRepository = selfFeedbackRepository;
            _mapper = mapper;
        }
        public async Task<List<GetFeedBackByUserIdQueryVm>> Handle(GetFeedBackByUserIdQuery request, CancellationToken cancellationToken)
        {
            int id = request.UserId;
            var userFeedbackData = await _selfFeedbackRepository.FindFeedbackDetailsByUserId(id);
            List<GetFeedBackByUserIdQueryVm> mapobject = _mapper.Map<List<GetFeedBackByUserIdQueryVm>>(userFeedbackData);
            return (mapobject);

        }
    }
  
}
