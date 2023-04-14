using Feedback360.Application.Features.AdminQuery.AdminQueries.GetAllQuery;
using Feedback360.Application.Features.AdminQuery.AdminQueries.GetCommentsById;
using Feedback360.Application.Features.AdminQuery.AdminQueries.GetQueryById;
using Feedback360.Application.Features.AdminQuery.Command;
using Feedback360.Application.Features.AdminQuery.Command.DeleteCommand;
using Feedback360.Application.Features.AdminQuery.Command.SetStatusCommand;
using Feedback360.Application.Features.RolePermission.Command.UpdatePermission;
using Feedback360.Application.Features.RolePermission.Query.GetAllPermissions;
using Feedback360.Application.Features.RolePermission.Query.GetPermissionsByRole;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Feedback360.Api.Controllers.v2
{
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        private readonly IMediator _mediatR;
        private readonly ILogger _logger;
        public QueryController(IMediator mediator, ILogger<QueryController> logger)
        {
            _mediatR = mediator;
            _logger = logger;

        }
        [HttpGet("GetAllAdminQueries", Name = "GetAllAdminQueries")]

        public async Task<ActionResult> GetAllAdminQueries()
        {
            var result = await _mediatR.Send(new GetAllAdminQueryCommand());
            return Ok(result);
        }

        [HttpGet("GetQueryById", Name = "GetQueryById")]

        public async Task<ActionResult> GetQueryById(int QueryId)
        {
            var dtos = await _mediatR.Send(new GetQueryByIdQuery() { QueryId = QueryId });
            return Ok(dtos);
        }
        [HttpPost("ResolveQuery", Name = "ResolveQuery")]
        public async Task<ActionResult> ResolveQuery(ResolveAdminQueryCommand resolveAdminQueryCommand)
        {
            var updatedData = await _mediatR.Send(resolveAdminQueryCommand);
            return Ok(updatedData);
        }
        //[HttpDelete("DeleteQuery",Name ="DeleteQuery")]
        //public async Task<ActionResult> DeleteQuery(List<int> QueryId)
        //{
        //   var result= await _mediatR.Send(new DeleteQueriesCommand() { QueryId=QueryId });
        //    return Ok(result);
        //}
        [HttpPost("DeleteQuery", Name = "DeleteQuery")]
        public async Task<ActionResult> DeleteQuery(List<int> QueryId)
        {
            var result = await _mediatR.Send(new DeleteQueriesCommand() { QueryId = QueryId });
            return Ok(result);
        }

        [HttpGet("GetAllCommentsById", Name = "GetAllCommentsById")]

        public async Task<ActionResult> GetAllCommentsById(int queryid)
        {
            var result = await _mediatR.Send(new GetCommentsByIdQuery() { QueryId = queryid});
            return Ok(result);
        }

        [HttpPost("SetStatus", Name = "SetStatus")]
        public async Task<ActionResult> SetStatus(SetStatusCommand setStatusCommand)
        {

            var result = await _mediatR.Send(setStatusCommand);
            return Ok(result);
        }

    }
}
