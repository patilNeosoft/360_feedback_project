using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.Announcements.Commands.DeleteAnnouncement;
using Feedback360.Application.Features.Banners.Commands.DeleteBanner;
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

namespace Feedback360.Application.UnitTests.Announcements.Commands
{
    public class DeleteAnnouncementTests
    {
        Mock<IMapper> mapper = new Mock<IMapper>();

        private readonly Mock<IAnnouncementsRepository> _announcementsRepository;
        public DeleteAnnouncementTests()
        {

            _announcementsRepository = AnnouncementsRepositoryMocks.AnnouncementsRepository();

        }

        [Fact]
        public async Task Handle_Deleted_FromAnnouncementsRepo()
        {
            var announcementId = _announcementsRepository.Object.ListAllAsync().Result.FirstOrDefault();
            var oldAnnouncement = await _announcementsRepository.Object.FindAnnouncementById(announcementId.AnnouncementId);
            var handler = new DeleteAnnouncementHandler(mapper.Object, _announcementsRepository.Object);
            await handler.Handle(new DeleteAnnouncementCommand() { AnnouncementId = oldAnnouncement.AnnouncementId }, CancellationToken.None);
            var allAnnouncements = await _announcementsRepository.Object.ListAllAsync();
            allAnnouncements.Count().ShouldBe(1);
            allAnnouncements.ShouldNotContain(oldAnnouncement);
        }

    }
}
