using Feedback360.Api.Controllers.v1;
using Feedback360.API.UnitTests.Mocks;
using Feedback360.Application.Features.UserQueries.Command.CreateUserComment;
using Feedback360.Application.Features.UserQueries.Command.CreateUserQuery;
using Feedback360.Application.Features.UserQueries.Queries.GetAllCommentsByQueryId;
using Feedback360.Application.Features.UserQueries.Queries.GetUserQueries;
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

namespace Feedback360.API.UnitTests.Controllers.v1
{
    public class UserQueryControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<ILogger<UserQueryController>> _mockLogger;
        public UserQueryControllerTests()
        {
            _mockMediator = MediatorMocks.GetMediator();
            _mockLogger = new Mock<ILogger<UserQueryController>>();


        }
        [Fact]
        public async Task Get_Queries_ByUserId()
        {
            var controller = new UserQueryController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.GetUserQuery(1);
            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.ShouldNotBeNull();
        }

        [Fact]
        public async Task Add_Query()
        {
            var controller = new UserQueryController(_mockMediator.Object, _mockLogger.Object);
            CreateUserQueryCommand createUserCommand = new CreateUserQueryCommand
            {
                QueryTitle = "Title 1",
                QueryStatus = true,
                Description = "description",
                UserId = 1
            };
            var result = controller.AddUserQuery(createUserCommand);
            result.ShouldNotBeNull();
            result.ShouldBeOfType<Task<ActionResult<bool>>>();
        }

        [Fact]
        public async Task AddUserComment()
        {
            var controller = new UserQueryController(_mockMediator.Object, _mockLogger.Object);
            CreateUserCommentCommand createUserComment = new CreateUserCommentCommand
            {
                QueryId = 1,
                CommentText = "text1",
                RoleName = "User",
            };
            var result = controller.AddUserComment(createUserComment);
            result.ShouldNotBeNull();
            result.ShouldBeOfType<Task<ActionResult<bool>>>();
        }

        
        [Fact]
        public async Task Get_Comments_ByQueryId()
        {
            var controller = new UserQueryController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.GetCommentsByQueryId(1);
            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<List<GetAllCommentsVm>>();
        }
        
        [Fact]
        public async Task GetSingleQuery_byQueryId()
        {
            var controller = new UserQueryController(_mockMediator.Object, _mockLogger.Object);
            var result = await controller.GetSingleQuery(1);
            result.ShouldBeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.ShouldBe(200);
            okObjectResult.Value.ShouldNotBeNull();
            okObjectResult.Value.ShouldBeOfType<Response<GetUserQueriesQueryVm>>();
        }
    }
}
