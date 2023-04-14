using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.UserRoles.Commands.CreateUserRole;
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

namespace Feedback360.Application.UnitTests.Admin.UserRole.Commands
{
    public class CreateUserRoleTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserRoleRepository> _mockUserRoleRepository;
        public CreateUserRoleTests()
        {
            _mockUserRoleRepository = UserRoleRepositoryMocks.UserRoleRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();

        }
        [Fact]
        public async Task Handle_ValidUserRole_AddedToUserRoleRepository()
        {
            var handler = new CreateUserRoleCommandHandler(_mapper, _mockUserRoleRepository.Object);
            var result = await handler.Handle(new CreateUserRoleCommand() { RoleName = "Test" }, CancellationToken.None);
            var res = result.ShouldBeOfType<Response<bool>>();
            result.Data.ShouldBeTrue();
            result.Succeeded.ShouldBe(true);
            result.Errors.ShouldBeNull();
        }
        
    }
}
