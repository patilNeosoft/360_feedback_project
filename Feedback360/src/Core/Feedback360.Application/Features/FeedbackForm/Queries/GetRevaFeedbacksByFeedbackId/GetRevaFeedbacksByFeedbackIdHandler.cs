using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.FeedbackForm.Queries.GetRepaFeedbacksByFeedbackId;
using Feedback360.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.FeedbackForm.Queries.GetRevaFeedbacksByFeedbackId
{
    public class GetRevaFeedbacksByFeedbackIdHandler : IRequestHandler<GetRevaFeedbacksByFeedbackIdQuery, List<FeedbackAnswerFormVM>>
    {
        private readonly IFeedbackFormRepository _feedbackFormRepository;
        public IMapper _mapper;
        public GetRevaFeedbacksByFeedbackIdHandler(IMapper mapper, IFeedbackFormRepository feedbackFormRepository)
        {
            _feedbackFormRepository = feedbackFormRepository;
            _mapper = mapper;
        }
        public async Task<List<FeedbackAnswerFormVM>> Handle(GetRevaFeedbacksByFeedbackIdQuery request, CancellationToken cancellationToken)
        {
            int id = request.FeedbackId;
            var feedbacks = await _feedbackFormRepository.RevaEditFeedBackForm(id);
            //var mapobject = _mapper.Map<List<FeedbackAnswerFormVM>>(feedbacks);
            return (feedbacks);
        }
    }
}
