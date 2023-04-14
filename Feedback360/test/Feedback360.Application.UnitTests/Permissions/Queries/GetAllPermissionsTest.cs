using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
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

namespace Feedback360.Application.UnitTests.Permissions.Queries
{
    public class GetAllPermissionsTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserPermissionRepository> _mockPermissionRepository;
        public GetAllPermissionsTest()
        {
            _mockPermissionRepository = PermissionRepositoryMocks.GetPermissionRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();

        }
        [Fact]
        public async Task Handle_ValidGetAllPermissions()
        {
            var handler = new GetAllPermissionsQueryHandler(_mockPermissionRepository.Object,_mapper);
            var result = await handler.Handle(new GetAllPermissionsQuery(), CancellationToken.None);
            result.ShouldBeOfType<Response<IEnumerable<GetAllPermissionsVM>>>();
            result.ShouldNotBeNull();

        }
    }
}
