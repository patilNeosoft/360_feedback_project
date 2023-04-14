using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Feedback360.Persistence.Models;
using Feedback360.Application.Features.Banks.Query;
using Feedback360.Domain.Entities;
using Feedback360.Infrastructure.EncryptDecrypt;

namespace Feedback360.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        private readonly IUserRepository _userRepository;
        private readonly ILoggedInUserService _loggedInUserService;
        private readonly ISendMailService _mailService;

        public UserController(IMediator mediator, ILogger<UserController> logger, IUserRepository userRepository, ILoggedInUserService loggedInUserService,ISendMailService mailService)
        {
            _mediator = mediator;
            _logger = logger;
            _userRepository = userRepository;
           _loggedInUserService = loggedInUserService;
            _mailService = mailService;
        }

      

        //public UserController(IUserRepository @object)
        //{
        //    this.@object = @object;
        //}

        [HttpPost("api/userlogin")]
        public async Task<AuthResponse> LoginUser(UserLoginVM userLoginVM)
        {
            var response = await _userRepository.UserLogin(userLoginVM.EmployeeId,userLoginVM.Password,userLoginVM.BankId);       
            return response;
        }

        [HttpGet("api/getbanklist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetAllBanks()
        {
            var dtos = await _mediator.Send(new GetBankListQuery());
            return Ok(dtos);
        }

        [HttpPost("ForgotPassword",Name = "ForgotPassword")]
        public async Task<ActionResult> ForgotPassword(ForgotPassword forgotPassword)
        {
            var response = await _userRepository.UserExists(forgotPassword.Email);
            string email = forgotPassword.Email;
            email = EncryptionDecryption.EncryptString(email);
            EmailEntity emailEntity = new EmailEntity()
            {
                Email = forgotPassword.Email,
                Subject = "Password Reset Link",
                Body = $"<a href = 'https://localhost:7207/User/ResetPassword?email={email}'>Click here to Reset</a>"
            };
            if(response != null)
            {
                var result = _mailService.SendMail(emailEntity);
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("ResetPassword", Name = "ResetPassword")]
        public async Task<ActionResult> ResetPassword(ResetPassword resetPassword)
        {
            resetPassword.Email = EncryptionDecryption.DecryptString(resetPassword.Email);
            var response = await _userRepository.ResetUserPassword(resetPassword);
            EmailEntity emailEntity = new EmailEntity()
            {
                Email = resetPassword.Email,
                Subject = "Password changed",
                Body = "Dear User, Your Password has been changed. If not done by you please contact Customer Support"
            };
            if(response.FirstName != null)
            {
               var result = _mailService.SendMail(emailEntity);
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost("ChangePassword", Name = "ChangePassword")]
        public async Task<ActionResult> ChangePassword(ChangePassword changePassword)
        {
            var result = await _userRepository.ChangePasswordAsync(changePassword.UserId, changePassword.OldPassword, changePassword.NewPassword);
            if(result != false)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
