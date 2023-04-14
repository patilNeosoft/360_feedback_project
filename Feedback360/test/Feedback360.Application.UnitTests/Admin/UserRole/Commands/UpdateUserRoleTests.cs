using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.UserRoles.Commands.UpdateUserRole;
using Feedback360.Application.Profiles;
using Feedback360.Application.UnitTests.Mocks;
using Feedback360.Persistence.Repositories;
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
    public class UpdateUserRoleTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserRoleRepository> _mockUserRoleRepository;

        public UpdateUserRoleTests()
        {
            _mockUserRoleRepository = UserRoleRepositoryMocks.UserRoleRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidEvent_UpdatedEvent()
        {
            var handler = new UpdateUserRoleCommandHandler(_mapper, _mockUserRoleRepository.Object);
            int RoleId = 1; 
            var newUserRole= new Domain.Entities.UserRole
            {

                RoleId = RoleId,
                RoleName = "TestRole",
                IsDeleted = false,
                IsActive = true
             };
            var oldRole = await _mockUserRoleRepository.Object.FindUserRoleById(RoleId);

            await handler.Handle(new UpdateUserRoleCommand()
            {
                RoleId = newUserRole.RoleId,
                RoleName = newUserRole.RoleName,
                IsDeleted = newUserRole.IsDeleted,
                IsActive = newUserRole.IsActive
            }, CancellationToken.None) ; 

            var allEvents = await _mockUserRoleRepository.Object.ListAllAsync();
            allEvents.Count.ShouldBe(1);
            oldRole.ShouldBeEquivalentTo(newUserRole);
        }

    }
}
