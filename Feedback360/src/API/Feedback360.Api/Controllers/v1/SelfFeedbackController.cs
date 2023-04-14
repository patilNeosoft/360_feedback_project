using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.SelfFeedBack.Query.GetFeedBackByUserId;
using Feedback360.Application.Features.SelfFeedBack.Query.GetSelfFeedbackSummary;
using Feedback360.Application.Features.SelfFeedBack.Query.GetUserAuthorityDataByUserId;
using Feedback360.Application.Features.SelfFeedBack.Query.GetUserFeedbackDataByFinancialYear;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Feedback360.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SelfFeedbackController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        private readonly ISelfFeedbackRepository _selfFeedbackRepository;
        
        public SelfFeedbackController(IMediator mediator, ILogger<UserController> logger, ISelfFeedbackRepository selfFeedbackRepository)
        {
            _mediator = mediator;
            _logger = logger;
            _selfFeedbackRepository = selfFeedbackRepository;
            
        }

        [HttpGet("GetUserAuthorityData", Name = "GetUserAuthorityData")]
        public async Task<ActionResult> GetUserAuthorityData(int UserId)
        {
            var dtos = await _mediator.Send(new GetUserAuthorityDataByUserIdQuery() { UserId = UserId });
            return Ok(dtos);
        }


        [HttpGet("GetUserFeedbackDetails", Name = "GetUserFeedbackDetails")]
        public async Task<ActionResult> GetUserFeedbackDetails(int UserId)
        {
            var dtos = await _mediator.Send(new GetFeedBackByUserIdQuery() { UserId = UserId });
            return Ok(dtos);
        }

        [HttpGet("GetUserFeedbackDetailsByFinancialYear", Name = "GetUserFeedbackDetailsByFinancialYear")]
        public async Task<ActionResult> FindAllFeedbacksByUser(int startYear,int endYear,int UserId)
        {
            var dtos = await _mediator.Send(new GetUserFeedbackDataByFinancialYearQuery() { StartYear = startYear,EndYear = endYear,UserId = UserId });
            return Ok(dtos);
        }

        [HttpGet("GetSelfFeedbackSummary", Name = "GetSelfFeedbackSummary")]
        public async Task<ActionResult> GetSelfFeedbackSummary(int UserId,int financialYear)
        {
            var dtos = await _mediator.Send(new GetSelfFeedbackSummaryQuery() { UserId = UserId,FinancialYear = financialYear });
            return Ok(dtos);
        }


    }
}
