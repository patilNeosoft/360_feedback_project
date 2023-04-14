using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.Banners.Commands.CreateBanner;
using Feedback360.Application.Profiles;
using Feedback360.Application.Responses;
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
    public class CreateBannerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IBannerRepository> _mockBannerRepository;
        
        public CreateBannerTests()
        {
            _mockBannerRepository = BannerRepositoryMocks.GetBannerRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidCategory_AddedToCategoryRepository()
        {
            var handler = new CreateBannerCommandHandler(_mapper, _mockBannerRepository.Object);

            var result = await handler.Handle(new CreateBannerCommand() { BannerTitle = "Test" }, CancellationToken.None);

            var allBanners = await _mockBannerRepository.Object.ListAllAsync();

            result.ShouldBeOfType<Response<CreateBannerDto>>();
            result.Succeeded.ShouldBe(true);
            result.Errors.ShouldBeNull();
           
        }

    }
}
