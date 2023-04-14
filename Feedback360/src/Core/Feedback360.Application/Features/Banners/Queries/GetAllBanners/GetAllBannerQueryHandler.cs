using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.Banners.Queries.GetAllBanners
{
    public class GetAllBannerQueryHandler: IRequestHandler<GetAllBannersQuery, IEnumerable<GetAllBannersVm>>
    {
        private readonly IBannerRepository _bannerRepository;
        public IMapper _mapper;
        public GetAllBannerQueryHandler(IMapper mapper, IBannerRepository bannerRepository)
        {
            _bannerRepository = bannerRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetAllBannersVm>> Handle(GetAllBannersQuery request, CancellationToken cancellationToken)
        {
            var allBanners = await _bannerRepository.ListAllBanners();
            IEnumerable<GetAllBannersVm> getAllBannersVms = _mapper.Map<IEnumerable<GetAllBannersVm>>(allBanners);
            return (getAllBannersVms).Where(u=> u.IsDeleted == false);

        }
    }
   
}
