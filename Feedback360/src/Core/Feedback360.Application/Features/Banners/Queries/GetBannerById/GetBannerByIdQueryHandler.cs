using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.Banners.Queries.GetBannerById
{
    public class GetBannerByIdQueryHandler: IRequestHandler<GetBannerByIdQuery,GetBannerByIdVm>
    {
        private readonly IBannerRepository _bannerRepository;
        public IMapper _mapper;
        public GetBannerByIdQueryHandler(IMapper mapper, IBannerRepository bannerRepository)
        {
            _bannerRepository = bannerRepository;
            _mapper = mapper;
        }
        public async Task<GetBannerByIdVm> Handle(GetBannerByIdQuery request, CancellationToken cancellationToken)
        {
            int id = request.BannerId;
            var banner = await _bannerRepository.FindBannerById(id);
            var mapobject = _mapper.Map<GetBannerByIdVm>(banner);
            return (mapobject);

        }
    }
    
}
