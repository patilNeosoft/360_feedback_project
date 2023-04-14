using Feedback360.Application.Contracts.Persistence;
using Feedback360.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.UnitTests.Mocks
{
    public class BannerRepositoryMocks
    {
        public static Mock<IBannerRepository> GetBannerRepository()
        {
            var mockBannerRepository = new Mock<IBannerRepository>();

            var banners = new List<Banner>
            {
                new Banner
                {
                    BannerId = 1,
                    BannerTitle = "latest offers",
                    BannerImageName = "December offers",
                    BannerImageUrl = "",
                    BankId = 1,
                    IsDeleted = false
                },
                 new Banner
                { 
                    BannerId = 2,
                    BannerTitle = "latest offers",
                    BannerImageName = "December offers",
                    BannerImageUrl = "",
                    BankId = 1,
                    IsDeleted = false
                }
            };
            //add new banner
            mockBannerRepository.Setup(repo => repo.AddAsync(It.IsAny<Banner>())).ReturnsAsync(

                (Banner banner) =>
                {
                    Banner newBanner = new Banner();
                    newBanner.BannerTitle = "Test1";
                    newBanner.IsDeleted = false;
                    newBanner.BannerImageUrl = "";
                    banners.Add(newBanner);
                    return newBanner;
                });

            //get all banners
            mockBannerRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(banners);

            //get banner by Id
            mockBannerRepository.Setup(repo => repo.FindBannerById(It.IsAny<int>())).ReturnsAsync(
                (int BannerId) =>
                {
                    return banners.SingleOrDefault(x => x.BannerId == 1);
                });

            //delete banner
            mockBannerRepository.Setup(repo => repo.RemoveBannerAsync(It.IsAny<Banner>())).Callback(
                (Banner banner) =>
                {
                    banner.IsDeleted = true;
                });

            //update banner
            mockBannerRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Banner>())).Callback(
                (Banner banner) =>
                {
                    var oldBanner = banners.First(x => x.BannerId == banner.BannerId);
                    var index = banners.IndexOf(oldBanner);
                    if (index != -1)
                        banners[index] = oldBanner;
                });
            return mockBannerRepository;
        }
    }

}

