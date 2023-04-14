
using Feedback360.Api.Controllers.v1;
using Feedback360.Api.Controllers.v2;
using Feedback360.API.UnitTests.Mocks;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.Announcements.Commands.CreateAnnouncement;
using Feedback360.Application.Features.Announcements.Commands.DeleteAnnouncement;
using Feedback360.Application.Features.Announcements.Commands.UpdateAnnouncement;
using Feedback360.Application.Features.Announcements.Queries.GetAllAnnouncements;
using Feedback360.Application.Features.Announcements.Queries.GetAnnouncementById;
using Feedback360.Application.Features.Banners.Commands.CreateBanner;
using Feedback360.Application.Features.Banners.Commands.DeleteBanner;
using Feedback360.Application.Features.Banners.Commands.UpdateBanner;
using Feedback360.Application.Features.Categories.Commands.CreateCategory;
using Feedback360.Application.Features.DashboardAnnouncement.Queries.GetAnnouncementForDashboard;
using Feedback360.Application.Features.Events.Commands.CreateEvent;
using Feedback360.Application.Features.UserRoles.Commands.CreateUserRole;
using Feedback360.Application.Features.UserRoles.Commands.DeleteUserRole;
using Feedback360.Application.Features.UserRoles.Queries.GetAllUserRoles;
using Feedback360.Application.Features.UserRoles.Queries.GetUserRoleById;
using Feedback360.Application.Responses;
using Feedback360.Persistence.Repositories;
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
    public class AnnouncementsControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<ILogger<AnnouncementsController>> _mockLogger;
        private readonly Mock<IAnnouncementsRepository> _announcementRepository;
        public AnnouncementsControllerTests()
        {
            _mockMediator = MediatorMocks.GetMediator();
            _mockLogger = new Mock<ILogger<AnnouncementsController>>();
            _announcementRepository = new Mock<IAnnouncementsRepository>();
        }

        [Fact]
        public async Task GetAll_Announcements()
        {
            var controller = new AnnouncementsController(_mockMediator.Object, _mockLogger.Object, _announcementRepository.Object);
            var result = await controller.GetAllAnnouncements();
            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
        }

        [Fact]
        public async Task Get_Announcement_ById()
        {
            var controller = new AnnouncementsController(_mockMediator.Object, _mockLogger.Object, _announcementRepository.Object);
            var result = await controller.GetAnnouncementById(1);
            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<GetAnnouncementByIdVm>();
        }

        [Fact]
        public async Task Get_DashboardAnnouncement_ByBankId()
        {
            var controller = new AnnouncementsController(_mockMediator.Object, _mockLogger.Object, _announcementRepository.Object);
            var result = await controller.GetDashboardAnnouncementById(1);
            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<GetDashboardAnnouncementByIdVm>();
        }

        [Fact]
        public async Task Add_Announcement()
        {   
            var controller = new AnnouncementsController(_mockMediator.Object, _mockLogger.Object, _announcementRepository.Object);
            CreateAnnouncementCommand createAnnouncementCommand = new CreateAnnouncementCommand
            {
                AnnouncementId = 1,
                Message = "abcd",
                IsActive = false,
                BankId = 1
            };
            var result = await controller.CreateAnnouncement(createAnnouncementCommand);
            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<CreateAnnouncementDto>>();
        }

        [Fact]
        public async Task Update_Announcement()
        {
            var controller = new AnnouncementsController(_mockMediator.Object, _mockLogger.Object, _announcementRepository.Object);
            var result = await controller.UpdateAnnouncement(new UpdateAnnouncementCommand());
            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<UpdateAnnouncementDto>();
        }

        [Fact]
        public async Task Delete_Announcement()
        {
            var controller = new AnnouncementsController(_mockMediator.Object, _mockLogger.Object, _announcementRepository.Object);
            var result = await controller.DeleteAnnouncement(1);
            result.ShouldBeOfType<OkObjectResult>();
            var response = result as OkObjectResult;
            response.StatusCode.ShouldBe(200);
            response.Value.ShouldBeOfType<DeleteAnnouncementDto>();
        }

    }
}
