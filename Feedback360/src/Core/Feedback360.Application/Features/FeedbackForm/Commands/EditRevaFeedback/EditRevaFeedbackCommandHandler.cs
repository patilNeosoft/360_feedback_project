using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.FeedbackForm.Commands.EditRepaFeedback;
using Feedback360.Application.Responses;
using Feedback360.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.FeedbackForm.Commands.EditRevaFeedback
{
    public class EditRevaFeedbackCommandHandler : IRequestHandler<EditRevaFeedbackCommand, Response<bool>>
    {
        private readonly IFeedbackFormRepository _feedbackFormRepository;
        public IMapper _mapper;
        public EditRevaFeedbackCommandHandler(IMapper mapper, IFeedbackFormRepository feedbackFormRepository)
        {
            _feedbackFormRepository = feedbackFormRepository;
            _mapper = mapper;
        }
        public async Task<Response<bool>> Handle(EditRevaFeedbackCommand request, CancellationToken cancellationToken)
        {
            FeedbackAnswerForm feedbackAnswerForm = await _feedbackFormRepository.GetRepaFeedback(request.QuestionId, request.FeedbackId);
            var feedbackAnswerForms = _mapper.Map(request, feedbackAnswerForm);
            await _feedbackFormRepository.RepaUpdateAsync(feedbackAnswerForms);

            return new Response<bool>(true);
        }
    }
}
