using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.UserQueries.Queries.GetAllCommentsByQueryId;
using Feedback360.Application.Features.UserQueries.Queries.GetUserQueries;
using Feedback360.Application.Profiles;
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

namespace Feedback360.Application.UnitTests.UserQueries.Queries
{
    public class GetAllCommentsByQueryIdTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserQueryRepository> _mockUserQueryRepository;

        public GetAllCommentsByQueryIdTests()
        {
            _mockUserQueryRepository = UserQueryRepositoryMocks.UserQueryRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidGetUserQueriesByQueryId()
        {
            var handler = new GetAllCommentsQueryHandler(_mapper, _mockUserQueryRepository.Object);
            var result = await handler.Handle(new GetAllCommentsByQueryIdQuery(), CancellationToken.None);
            result.ShouldBeOfType<List<GetAllCommentsVm>>();
            result.ShouldNotBeNull();

        }
    }
}
