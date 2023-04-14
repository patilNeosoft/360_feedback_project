using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.UserRoles.Commands.DeleteUserRole;
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
    public class DeleteUserRoleTests
    {
       
        Mock<IMapper> mapper = new Mock<IMapper>();

        private readonly Mock<IUserRoleRepository> _mockUserRoleRepository;
        public DeleteUserRoleTests()
        {
            
            _mockUserRoleRepository = UserRoleRepositoryMocks.UserRoleRepository();

        }

        [Fact]
        public async Task Handle_Deleted_FromEventsRepo()
        {
            var roleId = _mockUserRoleRepository.Object.ListAllAsync().Result.FirstOrDefault();
            var oldRole = await _mockUserRoleRepository.Object.FindUserRoleById(roleId.RoleId);
            var handler = new DeleteUserRoleCommandHandler(mapper.Object,_mockUserRoleRepository.Object);
            await handler.Handle(new DeleteUserRoleCommand() { RoleId = oldRole.RoleId}, CancellationToken.None);
            var allUserRoles = await _mockUserRoleRepository.Object.ListAllAsync();
            allUserRoles.ShouldNotContain(oldRole);
            
        }


    }
}
