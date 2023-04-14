using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.AdminQuery.AdminQueries.GetAllQuery;
using Feedback360.Application.Features.RolePermission.Query.GetAllPermissions;
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

namespace Feedback360.Application.UnitTests.AdminQueries.Queries
{
    public class GetAllAdminQueriesTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IQueryRepository> _mockQueryRepository;
        public GetAllAdminQueriesTest()
        {
            _mockQueryRepository = QueryRepositoryMocks.GetQueryRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();

        }
        [Fact]
        public async Task Handle_ValidGetAllAdminQueries()
        {
            var handler = new GetAllAdminQueryHandler( _mapper, _mockQueryRepository.Object);
            var result = await handler.Handle(new GetAllAdminQueryCommand(), CancellationToken.None);
            result.ShouldBeOfType<Response<IEnumerable<GetAllAdminQueryVM>>>();
            result.ShouldNotBeNull();

        }
    }
}

