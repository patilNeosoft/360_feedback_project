using Feedback360.Application.Features.AdminQuery.Command;
using Feedback360.Application.Features.Banners.Commands.UpdateBanner;
using Feedback360.Application.Features.FeedbackForm.Commands.AddFeedback;
using Feedback360.Application.Features.FeedbackForm.Commands.EditRepaFeedback;
using Feedback360.Application.Features.FeedbackForm.Commands.EditRevaFeedback;
using Feedback360.Application.Features.FeedbackForm.Commands.GetFeedbackId;
//using Feedback360.Application.Features.FeedbackForm.Queries.GetFeedbacksofRepaAndReva;
using Feedback360.Application.Features.FeedbackForm.Queries.GetQuestionsByBankId;
using Feedback360.Application.Features.FeedbackForm.Queries.GetRepaFeedbacksByFeedbackId;
using Feedback360.Application.Features.FeedbackForm.Queries.GetRepaUserFeedbacks;
using Feedback360.Application.Features.FeedbackForm.Queries.GetRevaFeedbacksByFeedbackId;
using Feedback360.Application.Features.FeedbackForm.Queries.GetRevaUserFeedbacks;
using Feedback360.Application.Features.UserQueries.Queries.GetAllCommentsByQueryId;
using Feedback360.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Feedback360.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FeedbackFormController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public FeedbackFormController(IMediator mediator, ILogger<FeedbackFormController> logger)
        {
            _mediator = mediator;
            _logger = logger;

        }

        [HttpGet("GetQuestionsByBankId", Name = "GetQuestionsByBankId")]
        public async Task<ActionResult> GetQuestionsByBankId(int bankid)
        {
            var questions = await _mediator.Send(new GetQuestionsByBankIdQuery() { BankId = bankid });
            return Ok(questions);
        }

        [HttpPost("AddFeedback", Name = "AddFeedback")]

        public async Task<ActionResult<bool>> AddFeedback(AddFeedbackCommand addFeedbackCommand)
        {
            var addedData = await _mediator.Send(addFeedbackCommand);
            return Ok(addedData);
        }


        //[HttpGet("GetFeedbacksOfRepoAndReva", Name = "GetFeedbacksOfRepoAndReva")]
        //public async Task<ActionResult> GetFeedbacksOfRepoAndReva(int userId)
        //{
        //    var feedbacklist = await _mediator.Send(new GetFeedbacksOfRepaAndRevaQuery() { UserId = userId });
        //    return Ok(feedbacklist);
        //}

        [HttpPost("GetFeedbackId",Name = "GetFeedbackId")]
        public async Task<ActionResult> GetFeedbackId(GetFeedbackIdCommand getFeedBackIdCommand)
        {
            var addedData = await _mediator.Send(getFeedBackIdCommand);
            return Ok(addedData);
        }

        [HttpGet("GetRepaUsers", Name = "GetRepaUsers")]
        public async Task<ActionResult> GetRepaUsers(int userId)
        {
          var res=  await _mediator.Send(new GetRepaUserFeedbackQuery() { UserId = userId });
        return Ok(res);
        }

        [HttpPut("EditRepaFeedbackList",Name = "EditRepaFeedbackList")]
        public async Task<ActionResult> EditRepaFeedbackList(EditRepaFeedbackCommand editRepaFeedbackCommand)
        {
            var updatedRepaFeedback = await _mediator.Send(editRepaFeedbackCommand);
            return Ok(updatedRepaFeedback);
        }

        [HttpGet("GetRepaFeedbacksByFeedbackId",Name = "GetRepaFeedbacksByFeedbackId")]
        public async Task<ActionResult> GetRepaFeedbacksByFeedbackId(int FeedbackId)
        {
            var Feedbacklist = await _mediator.Send(new GetRepaFeedbacksByFeedbackIdQuery() { FeedbackId = FeedbackId });
            return Ok(Feedbacklist);
        }


        [HttpGet("GetRevaUsers", Name = "GetRevaUsers")]
        public async Task<ActionResult> GetRevaUsers(int userId)
        {
            var res = await _mediator.Send(new GetRevaUserFeedbackQuery() { UserId = userId });
            return Ok(res);
        }

        [HttpPut("EditRevaFeedbackList", Name = "EditRevaFeedbackList")]
        public async Task<ActionResult> EditRevaFeedbackList(EditRevaFeedbackCommand editRevaFeedbackCommand)
        {
            var updatedRevaFeedback = await _mediator.Send(editRevaFeedbackCommand);
            return Ok(updatedRevaFeedback);
        }

        [HttpGet("GetRevaFeedbacksByFeedbackId", Name = "GetRevaFeedbacksByFeedbackId")]
        public async Task<ActionResult> GetRevaFeedbacksByFeedbackId(int FeedbackId)
        {
            var Feedbacklist = await _mediator.Send(new GetRevaFeedbacksByFeedbackIdQuery() { FeedbackId = FeedbackId });
            return Ok(Feedbacklist);
        }


    }
}
