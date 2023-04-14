using Castle.Core.Logging;
using DnsClient.Protocol;
//using FakeItEasy;
using Feedback360.Api.Controllers;
using Feedback360.Api.Controllers.v1;
using Feedback360.API.UnitTests.Mocks;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.UserRoles.Queries.GetUserRoleById;
using Feedback360.Application.Models.Authentication;
using Feedback360.Domain.Entities;
using Feedback360.Persistence.Models;
using MediatR;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NuGet.Protocol;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using YamlDotNet.Core;
using YamlDotNet.Core.Tokens;

namespace Feedback360.API.UnitTests.Controllers.v1
{
    public class UserLoginTest
    {
        private readonly Mock<IUserRepository> membership = new Mock<IUserRepository>();
        private readonly Mock<IMediator> mediator = new Mock<IMediator>();
        private readonly Mock<ILogger<UserController>> _logger = new Mock<ILogger<UserController>>();
        private readonly UserController userController;

        //public UserLoginTest()
        //{
        //    userController = new UserController(mediator.Object,membership.Object,_logger.Object);
        //}



        //[Fact]
        //public async void Loginusertest()
        //{
        //    Mock<IUserRepository> membership = new Mock<IUserRepository>();
        //    Mock<IMediator> mediator = new Mock<IMediator>();
        //    Mock<ILogger<UserController>> _logger= new Mock<ILogger<UserController>>();

        //    membership.Setup(m => m.UserLogin(It.IsAny<string>(), It.IsAny<string>(),It.IsAny<int>()))
        //              .ReturnsAsync(new AuthResponse());

        //    var logonModel = new UserLoginVM() { EmployeeId = "9535", Password = "Mitesh@123" ,BankId=1};
        //    var controller = new UserController(mediator.Object,membership.Object,_logger.Object);

        //    // Act


        //    var result = await controller.LoginUser(logonModel) as OkObjectResult;


        //    if (result is AuthResponse)
        //    {

        //        AuthResponse? auth = result.Value as AuthResponse;  
        //        var roleId = 1;
        //        auth.RoleId.Equals(roleId);
        //    }






        // Assert

        // result.ShouldNotBeNull();



        //  }

        [Fact]
        public async Task LoginUserTestCase()
        {
            AuthResponse authResponse = new AuthResponse
            {
                RoleId = 1
            };
            string employeeid = "9533";
            string password = "Mitesh@123";
            int bankid = 1;

            membership.Setup(x => x.UserLogin(employeeid, password, bankid)).ReturnsAsync(authResponse);
            var logonModel = new UserLoginVM() { EmployeeId = "9533", Password = "Mitesh@123", BankId = 1 };
           var auth= await userController.LoginUser(logonModel);
            Assert.Equal(authResponse.RoleId,auth.RoleId);


        }


    }

       
}
