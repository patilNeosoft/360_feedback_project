using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.Announcements.Commands.UpdateAnnouncement;
using Feedback360.Application.Features.Banners.Commands.UpdateBanner;
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

namespace Feedback360.Application.UnitTests.Announcements.Commands
{
    public class UpdateAnnouncementTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAnnouncementsRepository> _announcementsRepository;

        public UpdateAnnouncementTests()
        {
            _announcementsRepository = AnnouncementsRepositoryMocks.AnnouncementsRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidEvent_UpdatedAnnouncement()
        {
            var handler = new UpdateAnnouncementHandler(_mapper, _announcementsRepository.Object);
            var newAnnouncement = new Domain.Entities.Announcements
            {
                AnnouncementId=1,
                Message = "latest offers",
                IsActive = false,
                BankId = 1
            };
            //var oldBanner = await _announcementsRepository.Object.FindAnnouncementById(AnnouncementId);

            await handler.Handle(new UpdateAnnouncementCommand()
            {
                AnnouncementId = newAnnouncement.AnnouncementId,
                Message = newAnnouncement.Message,
                IsActive = newAnnouncement.IsActive,
                BankId = newAnnouncement.BankId
                
            }, CancellationToken.None);

            var allAnnouncements = await _announcementsRepository.Object.ListAllAsync();
            allAnnouncements.Count.ShouldBe(1);

        }
    }
}
