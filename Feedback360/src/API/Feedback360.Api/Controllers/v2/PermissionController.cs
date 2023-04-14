using Feedback360.Application.Features.RolePermission.Command.AddPermission;
using Feedback360.Application.Features.RolePermission.Command.UpdatePermission;
using Feedback360.Application.Features.RolePermission.Query.GetAllPermissions;
using Feedback360.Application.Features.RolePermission.Query.GetPermissionsByRole;
using Feedback360.Application.Features.UserRoles.Commands.CreateUserRole;
using Feedback360.Application.Features.UserRoles.Commands.UpdateUserRole;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Feedback360.Api.Controllers.v2
{
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]


    public class PermissionController : ControllerBase
    {
        private readonly IMediator _mediatR;
        private readonly ILogger _logger;
        public PermissionController(IMediator mediator, ILogger<PermissionController> logger)
        {
            _mediatR = mediator;
            _logger = logger;

        }
        [HttpPost("AddPermission", Name = "AddPersmission")]
        public async Task<ActionResult> AddPermission(AddRolePermissionCommand addPermissionCommand)
        {

            var response = await _mediatR.Send(addPermissionCommand);
            if (response.Succeeded)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpGet("GetAllPermissions",Name = "GetAllPermissions")]
     
        public async Task<ActionResult> GetAllPermissions()
        {
            var dtos = await _mediatR.Send(new GetAllPermissionsQuery());
            return Ok(dtos);
        }


        [HttpGet("GetAllPermissionsByRole", Name = "GetAllPermissionsByRole")]

        public async Task<ActionResult> GetAllPermissionsByRole(int Roleid)
        {
            var dtos = await _mediatR.Send(new GetPermissionByRoleQuery() { RoleId=Roleid });
            return Ok(dtos);
        }


        [HttpPut("UpdatePermissions", Name = "UpdatePermissions")]
        public async Task<ActionResult> UpdatePermissions(UpdatePermissionCommand updatePermissionCommand)
        {
            var updatedData = await _mediatR.Send(updatePermissionCommand);
            return Ok(updatedData);
        }
    }
}
