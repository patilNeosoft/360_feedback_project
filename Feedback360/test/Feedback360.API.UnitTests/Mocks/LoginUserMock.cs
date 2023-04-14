using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Models.Authentication;
using Feedback360.Domain.Entities;
using Feedback360.Persistence.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.API.UnitTests.Mocks
{
    public class LoginUserMock
    {
        private static string employeeid;
        private static string password;
        private static int bankid;

        public static  Mock<IUserRepository> GetAuthenticationService()
        {
            var mockAuthenticationService = new Mock<IUserRepository>();

          mockAuthenticationService.Setup(auth => auth.UserLogin(employeeid,password,bankid)).ReturnsAsync(new AuthResponse()
            {
                UserName = "UserName",
                RoleName= "RoleName",
               

            });

            return mockAuthenticationService;
        }
    }
}
