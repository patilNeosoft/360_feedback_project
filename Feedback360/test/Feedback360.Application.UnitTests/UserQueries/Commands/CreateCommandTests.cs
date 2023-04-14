using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.UserQueries.Command.CreateUserComment;
using Feedback360.Application.Profiles;
using Feedback360.Application.Responses;
using Feedback360.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Feedback360.Application.UnitTests.UserQueries.Commands
{
    public class CreateCommandTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserQueryRepository> _mockUserQueryRepository;
        public CreateCommandTests()
        {
            _mockUserQueryRepository = UserQueryRepositoryMocks.UserQueryRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidUserComment_AddedToUserQueryRepository()
        {
            var handler = new CreateUserCommentCommandHandler(_mapper, _mockUserQueryRepository.Object);
            var result = await handler.Handle(new CreateUserCommentCommand() {QueryId = 1, CommentText = "commentText", RoleName = "User"}, CancellationToken.None);
            result.ShouldBeOfType<Response<bool>>();
            result.Succeeded.ShouldBe(true);
            result.Errors.ShouldBeNull();
        }

    }
}
