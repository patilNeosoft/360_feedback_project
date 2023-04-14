using Feedback360.Api.Controllers.v2;
using Feedback360.API.UnitTests.Mocks;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.Banners.Commands.CreateBanner;
using Feedback360.Application.Features.Banners.Commands.DeleteBanner;
using Feedback360.Application.Features.Banners.Commands.UpdateBanner;
using Feedback360.Application.Features.Banners.Queries.GetAllBanners;
using Feedback360.Application.Features.Banners.Queries.GetBannerById;
using Feedback360.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Hosting;
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
    public class BannerControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<IWebHostEnvironment> _hostEnvironment;
        private readonly Mock<ILogger<BannerController>> _mockLogger;
        public BannerControllerTests()
        {
            _mockMediator = MediatorMocks.GetMediator();
            _hostEnvironment = new Mock<IWebHostEnvironment>();
            _mockLogger = new Mock<ILogger<BannerController>>();
            

        }
        [Fact]
        public async Task GetAll_Banners()
        {
            var controller = new BannerController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.GetAllBanners();
            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
        }

        [Fact]
        public async Task Add_Banner()
        {
            var controller = new BannerController(_mockMediator.Object, _mockLogger.Object);
            CreateBannerCommand createBannerCommand = new CreateBannerCommand
            {
                BankId = 1,
                BannerImageName = "abc",
                BannerImageUrl = "abc",
                BannerTitle = "title"
             };
            var result = controller.AddBanner(createBannerCommand);
            result.ShouldNotBeNull();
            result.ShouldBeOfType<Task<ActionResult<bool>>>();
            
        }

        [Fact]
        public async Task Get_Banner_ById()
        {
            var controller = new BannerController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.GetBannerById(1);
            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<GetBannerByIdVm>();
        }
        [Fact]
        public async Task Delete_Banner()
        {
            var controller = new BannerController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.DeleteBanner(1);
            result.ShouldBeOfType<OkObjectResult>();
            var response = result as OkObjectResult;
            response.StatusCode.ShouldBe(200);
            response.Value.ShouldBeOfType<DeleteBannerDto>();
        }

        [Fact]
        public async Task Update_Banner()
        {
            var controller = new BannerController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.UpdateBanner(new UpdateBannerCommand());
            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<UpdateBannerDto>();
        }
    }
}

