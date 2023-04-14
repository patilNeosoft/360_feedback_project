using Feedback360.Application.Contracts.Persistence;
using Feedback360.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.UnitTests.Mocks
{
    public class PermissionRepositoryMocks
    {

        public static Mock<IUserPermissionRepository> GetPermissionRepository()
        {
            var mockPermissinRepository = new Mock<IUserPermissionRepository>();

            var permisions = new List<RolePermissionMapping>
            {
                new RolePermissionMapping
                {
                   RoleId = 1,
                   PermissionId=1
                }
            };
            //add new permission
            mockPermissinRepository.Setup(repo => repo.AddAsync(It.IsAny<RolePermissionMapping>())).ReturnsAsync(

                (RolePermissionMapping rolePermissionMapping) =>
                {
                    RolePermissionMapping rolePermissionMappingObj = new RolePermissionMapping();
                    rolePermissionMappingObj.RoleId = 1;
                    rolePermissionMappingObj.PermissionId = 2;
                    permisions.Add(rolePermissionMappingObj);
                    return rolePermissionMappingObj;
                });

            //get all banners
            mockPermissinRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(permisions);

            //get banner by Id
            //mockBannerRepository.Setup(repo => repo.FindBannerById(It.IsAny<int>())).ReturnsAsync(
            //    (int BannerId) =>
            //    {
            //        return banners.SingleOrDefault(x => x.BannerId == 1);
            //    });

            //delete banner
            //mockBannerRepository.Setup(repo => repo.RemoveBannerAsync(It.IsAny<Banner>())).Callback(
            //    (Banner banner) =>
            //    {
            //        banner.IsDeleted = true;
            //    });

            //update banner
            //    mockBannerRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Banner>())).Callback(
            //        (Banner banner) =>
            //        {
            //            var oldBanner = banners.First(x => x.BannerId == banner.BannerId);
            //            var index = banners.IndexOf(oldBanner);
            //            if (index != -1)
            //                banners[index] = oldBanner;
            //        });
            return mockPermissinRepository;
        }
    }
}
