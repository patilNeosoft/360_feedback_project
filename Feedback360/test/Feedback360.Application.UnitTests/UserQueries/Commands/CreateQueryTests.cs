using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.UserQueries.Command.CreateUserQuery;
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
    public class CreateQueryTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserQueryRepository> _mockUserQueryRepository;

        public CreateQueryTests()
        {
            _mockUserQueryRepository = UserQueryRepositoryMocks.UserQueryRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidUserQuery_AddedToUserQueryRepository()
        {
            var handler = new CreateUserQueryCommandHandler(_mapper, _mockUserQueryRepository.Object);
            var result = await handler.Handle(new CreateUserQueryCommand() { QueryTitle = "Test3",QueryStatus = false,Description = "description3",UserId = 1 }, CancellationToken.None);
            //var allQueries = await _mockUserQueryRepository.Object.AddAsync();
            result.ShouldBeOfType<Response<CreateUserQueryDto>>();
            result.Succeeded.ShouldBe(true);
            result.Errors.ShouldBeNull();
        }

    }
}
