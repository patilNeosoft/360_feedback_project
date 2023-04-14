using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.FeedbackForm.Commands.AddFeedback;
using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.FeedbackForm.Commands.GetFeedbackId
{
    public class GetFeedbackIdCommandHandler : IRequestHandler<GetFeedbackIdCommand, int>
    {
        private readonly IFeedbackFormRepository _feedbackFormRepository;
        public IMapper _mapper;
        public GetFeedbackIdCommandHandler(IMapper mapper, IFeedbackFormRepository feedbackFormRepository)
        {
            _feedbackFormRepository = feedbackFormRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(GetFeedbackIdCommand request, CancellationToken cancellationToken)
        {
            var userfeedback = _mapper.Map<Feedback360.Domain.Entities.FeedbackUserMapping>(request);

            int feedbackid = await _feedbackFormRepository.AddUserFeedbackMap(userfeedback);
            return feedbackid;
        }
    }
}
