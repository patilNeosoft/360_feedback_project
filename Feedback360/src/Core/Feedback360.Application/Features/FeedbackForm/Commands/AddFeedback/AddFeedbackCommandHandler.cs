using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.UserQueries.Command.CreateUserComment;
using Feedback360.Application.Responses;
using Feedback360.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.FeedbackForm.Commands.AddFeedback
{
    public class AddFeedbackCommandHandler : IRequestHandler<AddFeedbackCommand, Response<bool>>
    {
        private readonly IFeedbackFormRepository _feedbackFormRepository;
        public IMapper _mapper;
        public AddFeedbackCommandHandler(IMapper mapper, IFeedbackFormRepository feedbackFormRepository)
        {
            _feedbackFormRepository = feedbackFormRepository;
            _mapper = mapper;
        }
        public async Task<Response<bool>> Handle(AddFeedbackCommand request, CancellationToken cancellationToken)
        {
            var feedback = _mapper.Map<Feedback360.Domain.Entities.FeedbackAnswerForm> (request);

            bool feedbackAdded = await _feedbackFormRepository.AddFeedback(feedback);
            return new Response<bool>(feedbackAdded);

        }
    }
}
