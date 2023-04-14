using Feedback360.Application.Features.UserAuthority.Commands.AddAuthority;
using Feedback360.Application.Features.UserAuthority.Queries.GetReporteeByBankId;
using Feedback360.Application.Features.UserAuthority.Queries.GetReviewingAuthorityByBankId;
using Feedback360.Application.Features.UserAuthority.Queries.GetUsersByBankId;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Feedback360.Api.Controllers.v2
{
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]

    public class UserAuthorityController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public UserAuthorityController(IMediator mediator, ILogger<UserAuthorityController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpGet("GetUsersByBankId", Name = "GetUsersByBankId")]
        public async Task<ActionResult> GetUsersByBankId(int bankId)
        {
            var getUsersByBankIdQuery = new GetUsersByBankIdQuery() { BankId = bankId };
            var response = await _mediator.Send(getUsersByBankIdQuery);
            return Ok(response);
        }
        [HttpPost("AddUserAuthority", Name = "AddUserAuthority")]
        public async Task<ActionResult> AddUserAuthority(AddAuthorityCommand authorityCommand)
        {
            bool isUserAuthorityAdded = false;
            var response = await _mediator.Send(authorityCommand);
            if(response.Data != null)
            {
                isUserAuthorityAdded = true;
            }
            return Ok(isUserAuthorityAdded);
        }
        [HttpGet("GetReporteeByBankId", Name = "GetReporteeByBankId")]
        public async Task<ActionResult> GetReporteeByBankId(int bankId)
        {
            var getReporteeByBankIdQuery = new GetReporteeByBankIdQuery() { BankId = bankId };
            var response = await _mediator.Send(getReporteeByBankIdQuery);
            return Ok(response);
        }
        [HttpGet("GetReviewingAuthorityByBankId", Name = "GetReviewingAuthorityByBankId")]
        public async Task<ActionResult> GetReviewingAuthorityByBankId(int bankId)
        {
            var getReviewingAuthorityByBankIdQuery = new GetReviewingAuthorityByBankIdQuery() { BankId = bankId };
            var response = await _mediator.Send(getReviewingAuthorityByBankIdQuery);
            return Ok(response);
        }
    }
}
