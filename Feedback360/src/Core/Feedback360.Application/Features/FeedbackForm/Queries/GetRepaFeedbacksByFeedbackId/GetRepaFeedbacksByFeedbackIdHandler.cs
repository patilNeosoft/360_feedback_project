using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.FeedbackForm.Queries.GetQuestionsByBankId;
using Feedback360.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.FeedbackForm.Queries.GetRepaFeedbacksByFeedbackId
{
    public class GetRepaFeedbacksByFeedbackIdHandler : IRequestHandler<GetRepaFeedbacksByFeedbackIdQuery, List<FeedbackAnswerFormVM>>
    {
        private readonly IFeedbackFormRepository _feedbackFormRepository;
        public IMapper _mapper;
        public GetRepaFeedbacksByFeedbackIdHandler(IMapper mapper, IFeedbackFormRepository feedbackFormRepository)
        {
            _feedbackFormRepository = feedbackFormRepository;
            _mapper = mapper;
        }
        public async Task<List<FeedbackAnswerFormVM>> Handle(GetRepaFeedbacksByFeedbackIdQuery request, CancellationToken cancellationToken)
        {
            int id = request.FeedbackId;
            var feedbacks = await _feedbackFormRepository.RepaEditFeedBackForm(id);
            //var mapobject = _mapper.Map<List<FeedbackAnswerFormVM>>(feedbacks);
            return (feedbacks);
        }
    }
}
