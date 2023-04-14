using AutoMapper;
using Azure;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.Announcements.Commands.CreateAnnouncement;
using Feedback360.Application.Features.Banners.Commands.CreateBanner;
using Feedback360.Application.Features.Categories.Commands.CreateCategory;
using Feedback360.Application.Features.UserRoles.Commands.CreateUserRole;
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
    public class CreateAnnouncementTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAnnouncementsRepository> _announcementsRepository;
        public CreateAnnouncementTests()
        {
            _announcementsRepository = AnnouncementsRepositoryMocks.AnnouncementsRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();

        }
        [Fact]
        public async Task Handle_ValidAnnouncement_AddedToAnnouncementRepository()
        {
            var handler = new CreateAnnouncementHandler(_mapper, _announcementsRepository.Object);

            var result = await handler.Handle(new CreateAnnouncementCommand() { Message = "Test" }, CancellationToken.None);

            var allAnnouncements = await _announcementsRepository.Object.ListAllAsync();

            result.ShouldBeOfType<Response<CreateAnnouncementDto>>();
            result.Succeeded.ShouldBe(true);
            result.Errors.ShouldBeNull();
        }
    }
}
