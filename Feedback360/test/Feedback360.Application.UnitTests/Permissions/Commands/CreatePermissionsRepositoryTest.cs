using AutoMapper;
using Castle.Core.Logging;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.Events.Commands.CreateEvent;
using Feedback360.Application.Features.RolePermission.Command.AddPermission;
using Feedback360.Application.Profiles;
using Feedback360.Application.Responses;
using Feedback360.Application.UnitTests.Mocks;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Feedback360.Application.UnitTests.Permissions.Commands
{
    public class CreatePermissionsRepositoryTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserPermissionRepository> _mockPermissionRepository;
        private readonly Mock<ILogger<AddRolePermissionCommandHandler>> _logger;


        public CreatePermissionsRepositoryTest()
        {
            _mockPermissionRepository = PermissionRepositoryMocks.GetPermissionRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
            _logger = new Mock<ILogger<AddRolePermissionCommandHandler>>();



        }

        [Fact]
        public async Task Handle_ValidPermissio_AddedToPermissionRepository()
        {
            var handler = new AddRolePermissionCommandHandler(_logger.Object, _mockPermissionRepository.Object, _mapper);
            List<int> permission = new List<int> { 1,2};
            var result = await handler.Handle(new AddRolePermissionCommand() { RoleId = 1, PermissionId = permission }, CancellationToken.None);

            var allpermissions = await _mockPermissionRepository.Object.ListAllAsync();

            result.ShouldBeOfType<Response<bool>>();
            result.Succeeded.ShouldBe(true);
            result.Errors.ShouldBeNull();

        }


    }
}
