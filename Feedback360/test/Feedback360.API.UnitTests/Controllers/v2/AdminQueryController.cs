using Feedback360.Api.Controllers.v2;
using Feedback360.API.UnitTests.Mocks;
using Feedback360.Application.Features.AdminQuery.AdminQueries.GetAllQuery;
using Feedback360.Application.Features.AdminQuery.Command;
using Feedback360.Application.Features.AdminQuery.Command.SetStatusCommand;
using Feedback360.Application.Features.Banners.Commands.DeleteBanner;
using Feedback360.Application.Responses;
using Feedback360.Application.UnitTests.AdminQueries.Queries;
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
    public class AdminQueryController
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<ILogger<QueryController>> _mockLogger;
        public AdminQueryController()
        {
            _mockMediator = MediatorMocks.GetMediator();
            _mockLogger = new Mock<ILogger<QueryController>>();


        }

        [Fact]
        public async Task Get_Queries_ByQueryId()
        {
            var controller = new QueryController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.GetQueryById(1);
            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.ShouldNotBeNull();
        }
        [Fact]
        public async Task GetAllAdminQueries()
        {
            var controller = new QueryController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.GetAllAdminQueries();
            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.Value.ShouldBeOfType<Response<IEnumerable<GetAllAdminQueryVM>>>();
           

        }

        [Fact]
        public async Task AddComment()
        {
            ResolveAdminQueryCommand resolveAdminQueryCommand = new ResolveAdminQueryCommand()
            {
                QueryId = 1,
                CommentDescription ="Text Demo",
                RoleName="Admin"
    };
            var controller = new QueryController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.ResolveQuery( resolveAdminQueryCommand);
           
            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.ShouldNotBeNull();


        }

        [Fact]
        public async Task DeleteQuery()
        {
            var controller = new QueryController(_mockMediator.Object, _mockLogger.Object);
            List<int> id = new List<int> { 1, 2 };
            var result = controller.DeleteQuery(id);
            result.ShouldNotBeNull();


        }


        [Fact]
        public async Task GetAllCommentsById()
        {
            var controller = new QueryController(_mockMediator.Object, _mockLogger.Object);
           var result= await controller.GetAllCommentsById(1);
            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.ShouldNotBeNull();

        }

        [Fact]
        public async Task SetStatus()
        {
            var controller = new QueryController(_mockMediator.Object, _mockLogger.Object);
            SetStatusCommand setStatusCommand = new SetStatusCommand() {QueryId = 1 };
           var result= await controller.SetStatus(setStatusCommand);

            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.ShouldNotBeNull();
        }
    }
}
