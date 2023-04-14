using Feedback360.Application.Features.UserRoles.Commands.CreateUserRole;
using Feedback360.Application.Features.UserRoles.Commands.DeleteUserRole;
using Feedback360.Application.Features.UserRoles.Commands.UpdateUserRole;
using Feedback360.Application.Features.UserRoles.Queries.GetAllUserRoles;
using Feedback360.Application.Features.UserRoles.Queries.GetUserRoleById;
using Feedback360.Application.Responses;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.FeedbackAdmin.Commands.RegisterAdmin;
using Feedback360.Persistence.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Feedback360.Domain.Entities;

namespace Feedback360.Api.Controllers.v2
{
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAdminRepository _adminRepository;
        private readonly ILogger _logger;
        public AdminController(IMediator mediator,ILogger<AdminController> logger, IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
            _mediator = mediator;
            _logger = logger;
        }


        [HttpPost("AddUserRole",Name = "AddUserRole")]
        public async Task<ActionResult> AddUserRole(CreateUserRoleCommand createUseRoleCommand)
        {
            var response = await _mediator.Send(createUseRoleCommand);
            return Ok(response);
         }

        [HttpPost("AdminLogin", Name = "AdminLogin")]
        public async Task<AuthResponse> AdminLogin(AdminLogin adminLogin)
        {
            var response = await _adminRepository.AdminLoginAsync(adminLogin.Email, adminLogin.Password);
            AuthResponse adminAuthResponse = new AuthResponse()
            {
                UserName = response.FirstName,
                RoleName = response.UserRole.RoleName,
                UserID = response.Id,
                RoleId = response.RoleId,
                BankId = response.BankId
            };
            return adminAuthResponse;
        }

        [HttpGet("GetAllUserRoles", Name = "GetAllUserRoles")]
        public async Task<ActionResult> GetAllUserRoles()
        {
            var response = await _mediator.Send(new GetAllUserRolesQuery());
            return Ok(response);
        }

        [HttpPost("RegisterAdmin", Name = "RegisterAdmin")]
        public async Task<ActionResult> RegisterAdmin([FromBody] RegisterAdminCommand registerAdminCommand)
        {
            var response = await _mediator.Send(registerAdminCommand);
            return Ok(response);
        }

        [HttpDelete("DeleteUserRole", Name = "DeleteUserRole")]
        public async Task<ActionResult> DeleteUserRole(int roleId)
        {
            var deleteUserRoleCommand = new DeleteUserRoleCommand() { RoleId = roleId };
            var response = await _mediator.Send(deleteUserRoleCommand);
            return Ok(response);

        }


        [HttpPut("UpdateUserRole", Name = "UpdateUserRole")]
        public async Task<ActionResult> UpdateUserRole(UpdateUserRoleCommand updateUserRoleCommand)
        {
            var updatedData = await _mediator.Send(updateUserRoleCommand);
            return Ok(updatedData);
        }


      


        [HttpGet("GetUserRoleById", Name = "GetUserRoleById")]
        public async Task<ActionResult> GetUserRoleById(int roleId)
        {
            var getUserRoleByIdQuery = new GetUserRoleByIdQuery() { RoleId = roleId };
            var response = await _mediator.Send(getUserRoleByIdQuery);
            return Ok(response);

        }

      

        


    }
}
