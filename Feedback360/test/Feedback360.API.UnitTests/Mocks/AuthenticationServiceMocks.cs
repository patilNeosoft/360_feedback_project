using Feedback360.Application.Contracts.Identity;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Models.Authentication;
using Feedback360.Persistence.Models;
using Moq;
using System;

namespace Feedback360.API.UnitTests.Mocks
{
    public class AuthenticationServiceMocks
    {
        public static Mock<IUserRepository> GetAuthenticationService()
        {
            var mockAuthenticationService = new Mock<IUserRepository>();
            string Password = "pooja";
            string EmployeeId = "1";
            int BankId = 1;

            mockAuthenticationService.Setup(auth => auth.UserLogin(Password, EmployeeId, BankId))  ;
            
            return mockAuthenticationService;
        }
    }
}
