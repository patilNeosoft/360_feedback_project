using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.Banners.Commands.DeleteBanner;
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

namespace Feedback360.Application.UnitTests.Banners.Commands
{
    public class DeleteBannerTests {

        private readonly IMapper _mapper;
        private readonly Mock<IBannerRepository> _mockBannerRepository;


        Mock<IMapper> mapper = new Mock<IMapper>();

        private readonly Mock<IUserRoleRepository> _mockUserRoleRepository;
        public DeleteBannerTests()
        {

            _mockBannerRepository = BannerRepositoryMocks.GetBannerRepository();

        }

        [Fact]
        public async Task Handle_Deleted_FromBannerRepo()
        {
            var bannerId = _mockBannerRepository.Object.ListAllAsync().Result.FirstOrDefault();
            var oldBanner = await _mockBannerRepository.Object.FindBannerById(bannerId.BannerId);
            var handler = new DeleteBannerCommandHandler(mapper.Object, _mockBannerRepository.Object);
            await handler.Handle(new DeleteBannerCommand() { BannerId = oldBanner.BannerId }, CancellationToken.None);
            var allUserRoles = await _mockBannerRepository.Object.ListAllAsync();
            allUserRoles.Count().ShouldBe(1);
            allUserRoles.Where(u => u.IsDeleted == true).Count().ShouldBe(1);

        }




    }
}
