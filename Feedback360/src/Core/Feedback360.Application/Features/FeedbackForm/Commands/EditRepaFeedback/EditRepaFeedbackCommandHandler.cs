using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.UserQueries.Command.CreateUserComment;
using Feedback360.Application.Responses;
using Feedback360.Domain.Entities;
using Feedback360.Domain.Models;
using MediatR;
using NuGet.Packaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.FeedbackForm.Commands.EditRepaFeedback
{
    public class EditRepaFeedbackCommandHandler:IRequestHandler<EditRepaFeedbackCommand, Response<bool>>
    {
        private readonly IFeedbackFormRepository _feedbackFormRepository;
        public IMapper _mapper;
        public EditRepaFeedbackCommandHandler(IMapper mapper, IFeedbackFormRepository feedbackFormRepository)
        {
            _feedbackFormRepository = feedbackFormRepository;
            _mapper = mapper;
        }

        public async Task<Response<bool>> Handle(EditRepaFeedbackCommand request, CancellationToken cancellationToken)
        {
            
            
            
            //foreach (var item in request.feedbackAnswerForms)
            //{
            //    feedbackId = item.FeedbackId;
            //    break;
            //}
            FeedbackAnswerForm feedbackAnswerForm = await _feedbackFormRepository.GetRepaFeedback(request.QuestionId,request.FeedbackId);
            var feedbackAnswerForms = _mapper.Map(request, feedbackAnswerForm);
            //List<FeedbackAnswerForm> feedbackAnswerFormsEntity = new List<FeedbackAnswerForm>();
           
            //List<FeedbackAnswerForm> feedbackAnswerFormEntity = _mapper.Map(feedbackAnswerForms, feedbackAnswerFormsEntity);
         
            //foreach (var item in feedbackAnswerFormEntity)
            //{

            //    await _feedbackFormRepository.RepaUpdateAsync(item);
            //}
            await _feedbackFormRepository.RepaUpdateAsync(feedbackAnswerForms);

            return new Response<bool>(true);

        }

    
    }
}

//private readonly IFeedbackFormRepository _feedbackFormRepository;
//public IMapper _mapper;
//public EditRepaFeedbackCommandHandler(IMapper mapper, IFeedbackFormRepository feedbackFormRepository)
//{
//    _feedbackFormRepository = feedbackFormRepository;
//    _mapper = mapper;
//}
//public async Task<Response<bool>> Handle(List<EditRepaFeedbackCommand> request, CancellationToken cancellationToken)
//{
//    int feedbackId = 0;
//    foreach(var item in request)
//    {
//        feedbackId = item.FeedbackId;
//        break;
//    }
//    List<FeedbackAnswerFormVM> feedbackAnswerFormVMList = await _feedbackFormRepository.RepaEditFeedBackForm(feedbackId);
//    var feedbackAnswerForms = _mapper.Map(request, feedbackAnswerFormVMList);
//    List<FeedbackAnswerForm> feedbackAnswerFormsEntity = new List<FeedbackAnswerForm>();
//    List<FeedbackAnswerForm> feedbackAnswerFormEntity = _mapper.Map(feedbackAnswerForms, feedbackAnswerFormsEntity);

//    foreach(var item in feedbackAnswerFormEntity)
//    {

//       await _feedbackFormRepository.RepaUpdateAsync(item);
//    }


//    return new Response<bool>(true);

//}