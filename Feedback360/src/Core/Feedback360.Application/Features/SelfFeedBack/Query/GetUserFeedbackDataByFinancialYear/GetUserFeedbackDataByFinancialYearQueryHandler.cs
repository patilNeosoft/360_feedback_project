using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.SelfFeedBack.Query.GetUserFeedbackDataByFinancialYear
{
    public class GetUserFeedbackDataByFinancialYearQueryHandler: IRequestHandler<GetUserFeedbackDataByFinancialYearQuery, List<GetUserFeedbackDataByFinancialYearVm>>
    {
        private readonly ISelfFeedbackRepository _selfFeedbackRepository;
        public IMapper _mapper;
        public GetUserFeedbackDataByFinancialYearQueryHandler(IMapper mapper, ISelfFeedbackRepository selfFeedbackRepository)
        {
            _selfFeedbackRepository = selfFeedbackRepository;
            _mapper = mapper;
        }
        public async Task<List<GetUserFeedbackDataByFinancialYearVm>> Handle(GetUserFeedbackDataByFinancialYearQuery request, CancellationToken cancellationToken)
        {
            var userFeedbackDataByFinancialYear = await _selfFeedbackRepository.FindAllFeedbacksByUser(request.StartYear,request.EndYear,request.UserId);
            List<GetUserFeedbackDataByFinancialYearVm> mapobject = _mapper.Map<List<GetUserFeedbackDataByFinancialYearVm>>(userFeedbackDataByFinancialYear);
            return (mapobject);

        }
    }
    
}
