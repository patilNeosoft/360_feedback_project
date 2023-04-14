using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
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

namespace Feedback360.Application.UnitTests.Banners.Commands
{
    public class UpdateBannerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IBannerRepository> _mockBannerRepository;


        public UpdateBannerTests()
        {
            _mockBannerRepository = BannerRepositoryMocks.GetBannerRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidEvent_UpdatedBanner()
        {
            var handler = new UpdateBannerCommandHandler(_mapper, _mockBannerRepository.Object);
            int BannerId = 1;
            var newBanner = new Domain.Entities.Banner
            {

                BannerTitle = "latest offers",
                BannerImageName = "December offers",
                BannerImageUrl = "",
                BankId = 1,
                IsDeleted = false

            };
            var oldBanner = await _mockBannerRepository.Object.FindBannerById(BannerId);

            await handler.Handle(new UpdateBannerCommand()
            {
                BannerId = newBanner.BannerId,
                BannerTitle = newBanner.BannerTitle,
                BannerImageUrl = newBanner.BannerImageUrl,
                BankId = newBanner.BankId,
                BannerImageName = newBanner.BannerImageName

            }, CancellationToken.None);

            var allBanners = await _mockBannerRepository.Object.ListAllAsync();
            allBanners.Count.ShouldBe(1);
            
        }


    }
}
