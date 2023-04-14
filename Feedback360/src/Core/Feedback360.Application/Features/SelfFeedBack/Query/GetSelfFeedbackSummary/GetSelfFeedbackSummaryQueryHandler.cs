using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.SelfFeedBack.Query.GetSelfFeedbackSummary
{
    public class GetSelfFeedbackSummaryQueryHandler: IRequestHandler<GetSelfFeedbackSummaryQuery, List<GetSelfFeedbackSummaryVm>>
    {
        private readonly ISelfFeedbackRepository _selfFeedbackRepository;
        public IMapper _mapper;
        public GetSelfFeedbackSummaryQueryHandler(IMapper mapper, ISelfFeedbackRepository selfFeedbackRepository)
        {
            _selfFeedbackRepository = selfFeedbackRepository;
            _mapper = mapper;
        }
        public async Task<List<GetSelfFeedbackSummaryVm>> Handle(GetSelfFeedbackSummaryQuery request, CancellationToken cancellationToken)
        {
            var userFeedbackDataByFinancialYear = await _selfFeedbackRepository.GetUserFeedbackSummary(request.UserId,request.FinancialYear);
            List<GetSelfFeedbackSummaryVm> mapobject = _mapper.Map<List<GetSelfFeedbackSummaryVm>>(userFeedbackDataByFinancialYear);
            return (mapobject);
        }
    }

}
