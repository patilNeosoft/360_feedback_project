using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.Announcements.Queries.GetAnnouncementById;
using Feedback360.Application.Features.UserRoles.Queries.GetUserRoleById;
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

namespace Feedback360.Application.UnitTests.Announcements.Queries
{
    public class GetAnnouncementByIdTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAnnouncementsRepository> _mockAnnouncementsRepository;
        public GetAnnouncementByIdTests()
        {
            _mockAnnouncementsRepository = AnnouncementsRepositoryMocks.AnnouncementsRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();

        }

        [Fact]
        public async Task Handle_ValidGetAnnouncementById()
        {
            var handler = new GetAnnouncementByIdHandler(_mapper, _mockAnnouncementsRepository.Object);
            var result = await handler.Handle(new GetAnnouncementByIdQuery(), CancellationToken.None);
            result.ShouldBeOfType<GetAnnouncementByIdVm>();
            result.ShouldNotBeNull();

        }
    }
}
