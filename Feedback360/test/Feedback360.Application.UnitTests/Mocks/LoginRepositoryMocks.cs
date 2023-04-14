using Feedback360.Application.Contracts.Persistence;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.UnitTests.Mocks
{
    public class LoginRepositoryMocks
    {
        public static Mock<IUserRepository> GetAuthenticationService()
        {
            var mockAuthenticationService = new Mock<IUserRepository>();
            string Password = "pooja";
            string EmployeeId = "1";
            int BankId = 1;

            mockAuthenticationService.Setup(auth => auth.UserLogin(Password, EmployeeId, BankId));

            return mockAuthenticationService;
        }
    }
}
