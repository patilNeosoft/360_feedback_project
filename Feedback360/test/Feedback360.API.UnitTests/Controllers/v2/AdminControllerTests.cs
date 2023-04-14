using Feedback360.Api.Controllers.v2;
using Feedback360.API.UnitTests.Mocks;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.UserRoles.Commands.CreateUserRole;
using Feedback360.Application.Features.UserRoles.Commands.DeleteUserRole;
using Feedback360.Application.Features.UserRoles.Commands.UpdateUserRole;
using Feedback360.Application.Features.UserRoles.Queries.GetAllUserRoles;
using Feedback360.Application.Features.UserRoles.Queries.GetUserRoleById;
using Feedback360.Application.Responses;
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

namespace Feedback360.API.UnitTests.Controllers.v2
{
    public class AdminControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<ILogger<AdminController>> _mockLogger;
        private readonly Mock<IAdminRepository> _adminRepository;
        public AdminControllerTests()
        {
            _mockMediator = MediatorMocks.GetMediator();
            _mockLogger = new Mock<ILogger<AdminController>>();
            _adminRepository = new Mock<IAdminRepository>();

        }
        [Fact]
        public async Task GetAll_UserRoles()
        {
            var controller = new AdminController(_mockMediator.Object, _mockLogger.Object,_adminRepository.Object);

            var result = await controller.GetAllUserRoles();

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<IEnumerable<GetAllUserRolesVm>>>();
        }
        
        [Fact]
        public async Task Add_UserRole()
        {
            CreateUserRoleCommand createUserRoleCommand = new CreateUserRoleCommand { 
            RoleName="Pooja",
            IsActive=true,
            IsDeleted=false
            };
            var controller = new AdminController(_mockMediator.Object, _mockLogger.Object, _adminRepository.Object);
            var result = await controller.AddUserRole(createUserRoleCommand);
            var res = result.ShouldBeOfType<OkObjectResult>();
            res.Value.ShouldBeOfType<Response<bool>>();
            res.Value.ShouldNotBe(false);
            
        }

        [Fact]
        public async Task Get_UserRole_ById()
        {
            var controller = new AdminController(_mockMediator.Object, _mockLogger.Object, _adminRepository.Object);
            var result = await controller.GetUserRoleById(1);
            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<GetUserRoleByIdVm>();
        }
        [Fact]
        public async Task Delete_UserRole()
        {
            var controller = new AdminController(_mockMediator.Object,_mockLogger.Object, _adminRepository.Object);

            var result = await controller.DeleteUserRole(1);

            result.ShouldBeOfType<OkObjectResult>();
            var response = result as OkObjectResult;
            response.StatusCode.ShouldBe(200);
            response.Value.ShouldBeOfType<Response<DeleteUserRoleDto>>();
        }

        [Fact]
        public async Task Update_UserRole()
        {
            var controller = new AdminController(_mockMediator.Object, _mockLogger.Object, _adminRepository.Object);

            var result = await controller.UpdateUserRole(new UpdateUserRoleCommand());

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<UpdateUserRoleDto>>();
        }
    }
}
