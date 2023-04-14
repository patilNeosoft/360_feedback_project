using Feedback360.Application.Contracts;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Feedback360.Api.Services
{
    public class LoggedInUserService:ILoggedInUserService
    {
        public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserRole = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);
        }

        public string UserRole { get; }
    }
}
