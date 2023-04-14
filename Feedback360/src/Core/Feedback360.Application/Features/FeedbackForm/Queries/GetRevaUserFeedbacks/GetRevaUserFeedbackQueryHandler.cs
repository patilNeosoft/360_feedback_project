using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.FeedbackForm.Queries.GetRepaUserFeedbacks;
using Feedback360.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.FeedbackForm.Queries.GetRevaUserFeedbacks
{
    public class GetRevaUserFeedbackQueryHandler : IRequestHandler<GetRevaUserFeedbackQuery, List<PresentUserFeedbacks>>
    {
        private readonly IFeedbackFormRepository _feedbackFormRepository;
        public IMapper _mapper;
        public GetRevaUserFeedbackQueryHandler(IMapper mapper, IFeedbackFormRepository feedbackFormRepository)
        {
            _feedbackFormRepository = feedbackFormRepository;
            _mapper = mapper;
        }
        public async Task<List<PresentUserFeedbacks>> Handle(GetRevaUserFeedbackQuery request, CancellationToken cancellationToken)
        {
            List<PresentUserFeedbacks> presentUserFeedbacklist = await _feedbackFormRepository.RevaUserList(request.UserId);
            return presentUserFeedbacklist;

        }
    }
}
