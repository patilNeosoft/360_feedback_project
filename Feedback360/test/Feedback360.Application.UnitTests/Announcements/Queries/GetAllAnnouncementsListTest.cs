using AutoMapper;
using Azure;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.Announcements.Queries.GetAllAnnouncements;
using Feedback360.Application.Features.UserRoles.Queries.GetAllUserRoles;
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
    public class GetAllAnnouncementsListTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAnnouncementsRepository> _announcementsRepository;
        public GetAllAnnouncementsListTest()
        {
            _announcementsRepository = AnnouncementsRepositoryMocks.AnnouncementsRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();

        }
        [Fact]
        public async Task Handle_ValidGetAllAnnouncements()
        {
            var handler = new GetAllAnnouncementsHandler(_mapper, _announcementsRepository.Object);
            var result = await handler.Handle(new GetAllAnnouncementsQuery(), CancellationToken.None);
            result.ShouldBeOfType<Response<IEnumerable<GetAllAnnouncementsVm>>>();
            //result.Data.ShouldNotBeEmpty();
            //result.Succeeded.ShouldBe(true);
            //result.Errors.ShouldBeNull();
        }
    }
}
