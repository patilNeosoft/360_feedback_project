using FakeItEasy;
using Feedback360.Api.Controllers.v2;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Domain.Entities;
using Feedback360.Persistence;
using Feedback360.Persistence.Models;
using Feedback360.Persistence.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Logging;
//using Feedback360.Domain.Entities;
using Moq;
using Xunit;

namespace FeedbackUnitTest
{
    public class UnitTest1
    {
        Mock<IMediator> _mediator = new Mock<IMediator>();
        Mock<ILogger<User>> _logger = new Mock<ILogger<User>>();
        Mock<ApplicationDbContext> _dbContext = new Mock<ApplicationDbContext>();
        [Fact]
        public async void AdminLogintest()
        {
            #region
            //Mock<IAdminRepository> membership = new Mock<IAdminRepository>();
            //membership.Setup(m => m.AdminLoginAsync(It.IsAny<string>(), It.IsAny<string>()))
            //          .ReturnsAsync(new User());
            //// Mock<IFormsService> forms = new Mock<IFormsService>();
            //var controller = new AdminRepository(_dbContext, _logger);

            //// Act
            //var result = await controller.AdminLogin(logonModel) as OkObjectResult;

            //var testAdmin = result.GetType().GetProperties();
            //// Assert
            ////result.Equals;
            ////Assert.Equal(result.Id,1);
            //Assert.Equal(testAdmin.Length, 11);
            ////Assert.Equal(testAdmin.Contains(testAdmin.));
            #endregion

            #region
            Mock<IAdminRepository> membership = new Mock<IAdminRepository>();
            membership.Setup(m => m.AdminLoginAsync(It.IsAny<string>(), It.IsAny<string>()))
                      .ReturnsAsync(new User()
                      {
                          Id = 69,
                          Email = "Admin@gmail.com"
                      });
            // Mock<IFormsService> forms = new Mock<IFormsService>();
            var logonModel = new AdminLogin() { Email = "Admin@gmail.com", Password = "Admin@123" };
            var controller = new AdminController(_mediator.Object,membership.Object);

            // Act
            var result = await controller.AdminLogin(logonModel);
            Console.WriteLine(result);

            Assert.Equal(result.Email, logonModel.Email);
            // Assert
            //Assert.NotNull(result);
            #endregion



        }
    }
}