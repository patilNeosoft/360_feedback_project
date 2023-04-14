using Feedback360.Api.Services;
using Feedback360.Application.Contracts;
using Feedback360.Application.Features.DepartmentTeams.Command.addEmployeeToGroup;
using Feedback360.Application.Features.DepartmentTeams.Command.DropGroup;
using Feedback360.Application.Features.DepartmentTeams.Command.RemoveEmployeeFromGroup;
using Feedback360.Application.Features.DepartmentTeams.Query.GetActiveFinancialYear;
using Feedback360.Application.Features.DepartmentTeams.Query.GetEmployeeListByDepId;
using Feedback360.Application.Features.DepartmentTeams.Query.GetGroupMembersList;
using Feedback360.Application.Features.DepartmentTeams.Query.GetSecondaryRoleList;
using Feedback360.Application.Features.DepartmentTeams.Query.GetTeamLeader;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Feedback360.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DepartmentTeamController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public DepartmentTeamController(IMediator mediator, ILogger<DepartmentTeamController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("AddMemberToGroup", Name = "AddMemberToGroup")]
        public async Task<ActionResult<bool>> AddFeedback(addEmployeeToGroupCommand addEmployeeCommand)
        {
            var addedData = await _mediator.Send(addEmployeeCommand);
            return Ok(addedData);
        }

        [HttpDelete("RemoveTeamMember", Name = "RemoveTeamMember")]
        public async Task<ActionResult> RemoveMember(int depTeamId)
        {
            var removeMemberCommand = new RemoveEmployeeFromGroupCommand() { DeptTeamId = depTeamId };
            var response = await _mediator.Send(removeMemberCommand);
            return Ok(response);
        }


        [HttpGet("GetAllAvailableUsers", Name = "GetAllAvailableUsers")]
        public async Task<ActionResult> GetAllAvailableUsersList(int bankId,int userId)
        {
            var getAllUsersQuery = new GetEmployeeListByDepIdQuery() {BankId = bankId,UserId = userId };
            var response = await _mediator.Send(getAllUsersQuery);
            return Ok(response);
        }

        [HttpGet("GetTeamMembers", Name = "GetTeamMembers")]
        public async Task<ActionResult> GetTeamMembersList(int userId)
        {
            var getAllMembersQuery = new GetGroupMemberListQuery() {TeamLeadUserId = userId };
            var response = await _mediator.Send(getAllMembersQuery);
            return Ok(response);
        }

        [HttpGet("GetAllSecondaryRoles", Name = "GetAllSecondaryRoles")]
        public async Task<ActionResult> GetAllSecondaryRolesList()
        {
            var getAllSecondaryRolesQuery = new GetSecondaryroleListQuery() {};
            var response = await _mediator.Send(getAllSecondaryRolesQuery);
            return Ok(response);
        }

        [HttpGet("GetTLDetails", Name = "GetTLDetails")]
        public async Task<ActionResult> GetTeamLeaderDetails(int teamLeadId)
        {
            var getTLDetailsQuery = new GetTeamLeaderQuery() { TeamLeadId = teamLeadId };
            var response = await _mediator.Send(getTLDetailsQuery);
            return Ok(response);
        }

        [HttpGet("GetFinancialYear", Name = "GetFinancialYear")]
        public async Task<ActionResult> GetCurrentFinancialYear()
        {
            var getFinancialYearQuery = new GetActiveFinancialYearQuery() { };
            var response = await _mediator.Send(getFinancialYearQuery);
            return Ok(response);
        }

        [HttpDelete("DropGroup", Name = "DropGroup")]
        public async Task<ActionResult> DropGroupByTL(int teamLeadId)
        {
            var dropGroupCommand = new DropGroupCommand() { TeamLeadId = teamLeadId };
            var response = await _mediator.Send(dropGroupCommand);
            return Ok(response);
        }

    }
}
