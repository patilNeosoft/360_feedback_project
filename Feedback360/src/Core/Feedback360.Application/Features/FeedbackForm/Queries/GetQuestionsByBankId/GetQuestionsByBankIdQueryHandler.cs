using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.Banners.Queries.GetBannersByBankId;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.FeedbackForm.Queries.GetQuestionsByBankId
{
    public class GetQuestionsByBankIdQueryHandler: IRequestHandler<GetQuestionsByBankIdQuery, List<GetQuestionsByBankIdQueryVM>>
    {
        private readonly IFeedbackFormRepository _feedbackFormRepository;
        public IMapper _mapper;
        public GetQuestionsByBankIdQueryHandler(IMapper mapper, IFeedbackFormRepository feedbackFormRepository)
        {
            _feedbackFormRepository = feedbackFormRepository;
            _mapper = mapper;
        }

        public async Task<List<GetQuestionsByBankIdQueryVM>> Handle(GetQuestionsByBankIdQuery request, CancellationToken cancellationToken)
        {
            int id = request.BankId;
            var questions = await _feedbackFormRepository.FindQuestionsByBankId(id);
            var mapobject = _mapper.Map<List<GetQuestionsByBankIdQueryVM>>(questions);
            return (mapobject);
        }


    }
}
