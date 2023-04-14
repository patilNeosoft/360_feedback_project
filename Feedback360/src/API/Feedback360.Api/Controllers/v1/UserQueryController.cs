using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.UserQueries.Command.CreateUserComment;
using Feedback360.Application.Features.UserQueries.Command.CreateUserQuery;
using Feedback360.Application.Features.UserQueries.Command.DeleteUserQuery;
using Feedback360.Application.Features.UserQueries.Queries.GetAllCommentsByQueryId;
using Feedback360.Application.Features.UserQueries.Queries.GetUserQueries;
using Feedback360.Application.Features.UserQueries.Queries.GetUserQueryDataByUserId;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Feedback360.Api.Controllers.v1
{
    
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserQueryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        
        public UserQueryController(IMediator mediator, ILogger<UserQueryController> logger)
        {
            _mediator = mediator;
            _logger = logger;
            
        }


        [HttpPost("AddUserQuery", Name = "AddUserQuery")]
        public async Task<ActionResult<bool>> AddUserQuery(CreateUserQueryCommand createUserQueryCommand)
        {
            bool IsUserQueryAdded = false;
            var response = await _mediator.Send(createUserQueryCommand);
            if ((response.Data.QueryId != null) && (response.Data.Description != null))
            {
                IsUserQueryAdded = true;

            }
            return Ok(IsUserQueryAdded);
        }

        [HttpPost("AddUserComment", Name = "AddUserComment")]
        public async Task<ActionResult<bool>> AddUserComment(CreateUserCommentCommand createUserCommentCommand)
        {
            var response = await _mediator.Send(createUserCommentCommand);
            if (response.Succeeded && response.Data == true)
            {
                return true;
            }
            return false;
        }

        [HttpGet("GetQueriesByUserId", Name = "GetQueriesByUserId")]
        public async Task<ActionResult> GetUserQuery(int UserId)
        {
            var allQueries = await _mediator.Send(new GetUserQueryByUserIdQuery() { UserId = UserId });
            return Ok(allQueries);
        }

        [HttpGet("GetQuery", Name = "GetQuery")]
        public async Task<ActionResult> GetSingleQuery(int queryId)
        {
            var query = await _mediator.Send(new GetUserQueriesQuery() { QueryId = queryId });
            return Ok(query);
        }

        [HttpDelete("DeleteUserQuery", Name = "DeleteUserQuery")]
        public async Task<ActionResult> DeleteUserQuery(int queryId)
        {
            var deleteUserQueryCommand = new DeleteUserQueryCommand() { QueryId = queryId };
            var response = await _mediator.Send(deleteUserQueryCommand);
            return Ok(response);

        }

        [HttpGet("GetCommentsByQueryId", Name = "GetCommentsByQueryId")]
        public async Task<ActionResult> GetCommentsByQueryId(int queryId)
        {
            var query = await _mediator.Send(new GetAllCommentsByQueryIdQuery() { QueryId = queryId });
            return Ok(query);
        }
    }
}
