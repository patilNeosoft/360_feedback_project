using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.UserRoles.Queries.GetAllUserRoles;
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

namespace Feedback360.Application.UnitTests.Admin.UserRole.Queries
{
    public class GetAllUserRoleListTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserRoleRepository> _mockUserRoleRepository;
        public GetAllUserRoleListTests()
        {
            _mockUserRoleRepository = UserRoleRepositoryMocks.UserRoleRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();

        }
        [Fact]
        public async Task Handle_ValidGetAllUserRoles()
        {
            var handler = new GetAllUserRolesQueryHandler(_mapper, _mockUserRoleRepository.Object);
            var result = await handler.Handle(new GetAllUserRolesQuery(), CancellationToken.None);
            result.ShouldBeOfType<Response<IEnumerable<GetAllUserRolesVm>>>();
            result.Data.ShouldNotBeEmpty();
             result.Succeeded.ShouldBe(true);
            result.Errors.ShouldBeNull();
         }

    }
}
