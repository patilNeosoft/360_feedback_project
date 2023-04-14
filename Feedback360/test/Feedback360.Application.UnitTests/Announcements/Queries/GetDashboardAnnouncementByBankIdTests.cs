using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.Announcements.Queries.GetAnnouncementById;
using Feedback360.Application.Features.DashboardAnnouncement.Queries.GetAnnouncementForDashboard;
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
    public class GetDashboardAnnouncementByBankIdTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAnnouncementsRepository> _mockAnnouncementsRepository;
        public GetDashboardAnnouncementByBankIdTests()
        {
            _mockAnnouncementsRepository = AnnouncementsRepositoryMocks.AnnouncementsRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();

        }

        [Fact]
        public async Task Handle_ValidGetDashboardAnnouncementByBankId()
        {
            var handler = new GetDashboardAnnouncementByIdHandler(_mapper, _mockAnnouncementsRepository.Object);
            var result = await handler.Handle(new GetDashboardAnnouncementByIdQuery(), CancellationToken.None);
            result.ShouldBeOfType<GetDashboardAnnouncementByIdVm>();
            result.ShouldNotBeNull();

        }
    }
}
