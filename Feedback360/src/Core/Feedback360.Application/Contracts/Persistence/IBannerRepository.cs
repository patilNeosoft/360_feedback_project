using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feedback360.Domain.Entities;

namespace Feedback360.Application.Contracts.Persistence
{
    public interface IBannerRepository: IAsyncRepository<Banner>
    {
        public Task<Banner> FindBannerById(int id);
        public Task<IReadOnlyList<Banner>> ListAllBanners();
        public Task<IReadOnlyList<Banner>> FindBannersByBankId(int bankId);
        public Task RemoveBannerAsync(Banner banner);
    }
}
