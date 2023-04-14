using Feedback360.Application.Contracts.Persistence;
using Feedback360.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.UnitTests.Mocks
{
    public class UserRepositoryMocks
    {
        public static Mock<IUserRepository> GetUserRepository()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var users = new List<User>
            {
                new User
                {
                    Id = 1,
                    RoleId = 1,
                    BankId = 1,
                    FirstName = "Abc",
                    LastName = "Xyz",
                    Email = "abc@gmail.com",
                    EmployeeId = "9566",
                    Organization = "Neo",
                    Password = "Abcd@123",
                    ContactNumber = "0987654321"
                }
            };
            //Add a new User
            mockUserRepository.Setup(repo => repo.AddAsync(It.IsAny<User>())).ReturnsAsync(
                (User user) =>
                {
                    User user1 = new User();
                    user1.Id = 2;
                    user1.RoleId = 1;
                    user1.BankId = 1;
                    user1.FirstName = "Xyzz";
                    user1.LastName = "Abcc";
                    user1.Email = "xyzz@gmail.com";
                    user1.EmployeeId = "9669";
                    user1.Organization = "Neoo";
                    user1.Password = "Xyzz@321";
                    user1.ContactNumber = "1234567890";
                    users.Add(user1);
                    return user1;
                });

            //Change Password for User
            //mockUserRepository.Setup(repo => repo.ChangePasswordAsync(It.IsAny<int,string,string>())).
            return mockUserRepository;
        }
    }
}
