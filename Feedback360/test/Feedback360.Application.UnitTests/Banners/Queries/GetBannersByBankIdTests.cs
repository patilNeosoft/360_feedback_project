using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.Banners.Queries.GetBannersByBankId;
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

namespace Feedback360.Application.UnitTests.Banners.Queries
{
    public class GetBannersByBankIdTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IBannerRepository> _mockBannerRepository;
        public GetBannersByBankIdTests()
        {
            _mockBannerRepository = BannerRepositoryMocks.GetBannerRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();

        }
        [Fact]
        public async Task Handle_ValidGetBannersByBankId()
        {
            var handler = new GetBannersByBankIdQueryHandler(_mapper, _mockBannerRepository.Object);
            var result = await handler.Handle(new GetBannersByBankIdQuery(), CancellationToken.None);
            result.ShouldBeOfType<List<GetBannersByBankIdVm>>();
            result.ShouldNotBeNull();

        }
    }
}

