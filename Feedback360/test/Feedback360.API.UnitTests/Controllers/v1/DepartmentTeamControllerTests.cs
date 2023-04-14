using Feedback360.Api.Controllers.v1;
using Feedback360.API.UnitTests.Mocks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Feedback360.API.UnitTests.Controllers.v1
{
    public class DepartmentTeamControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<ILogger<DepartmentTeamController>> _mockLogger;
        public DepartmentTeamControllerTests()
        {
            _mockMediator = MediatorMocks.GetMediator();
            _mockLogger = new Mock<ILogger<DepartmentTeamController>>();


        }
        [Fact]
        public async Task Get_DepartmentTeam()
        {
            var controller = new DepartmentTeamController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.GetTeamMembersList(1);
            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.ShouldNotBeNull();
        }
        [Fact]
        public async Task Get_AllMembersFromTeam()
        {
            var controller = new DepartmentTeamController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.GetTeamLeaderDetails(1);
            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.ShouldNotBeNull();
        }
        [Fact]
        
        public async Task Get_AllSecondaryRoles()
        {
            var controller = new DepartmentTeamController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.GetAllSecondaryRolesList();
            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.ShouldNotBeNull();
        }
        [Fact]
        public async Task Get_AllMembersFromSameDepartment()
        {
            var controller = new DepartmentTeamController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.GetAllAvailableUsersList(1,1);
            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.ShouldNotBeNull();
        }

        [Fact]
        public async Task Remove_Team_Member()
        {
            var controller = new DepartmentTeamController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.RemoveMember(1);
            result.ShouldBeOfType<OkObjectResult>();
            var response = result as OkObjectResult;
            response.StatusCode.ShouldBe(200);
           
        }

        [Fact]
        public async Task Get_FinancialYear()
        {
            var controller = new DepartmentTeamController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.GetCurrentFinancialYear();
            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.ShouldNotBeNull();
        }

    }
}
