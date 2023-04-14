using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Persistence.Repositories
{
    public class BannerRepository : BaseRepository<Banner>, IBannerRepository
    {
        public IMapper _mapper;
        protected readonly ApplicationDbContext _dbContext;
        public BannerRepository(IMapper mapper, ApplicationDbContext dbContext, ILogger<Banner> logger) : base(dbContext, logger)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<Banner> FindBannerById(int id)
        {
            Banner banner = await _dbContext.Banners.Where(u => u.BannerId == id).FirstOrDefaultAsync();
            return banner;
        }
        public async Task<IReadOnlyList<Banner>> ListAllBanners()
        {
            return await _dbContext.Banners.Include(x => x.Bank).ToListAsync();
        }

        public async Task<IReadOnlyList<Banner>> FindBannersByBankId(int bankId)
        {
            return await _dbContext.Banners.Where(u => u.BankId == bankId && u.IsDeleted==false).ToListAsync();
        }
        public async Task RemoveBannerAsync(Banner banner)
        {
            banner.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
        }


    }

}

