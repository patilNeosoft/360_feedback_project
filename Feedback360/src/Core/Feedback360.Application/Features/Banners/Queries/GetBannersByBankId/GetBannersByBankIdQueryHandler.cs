using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.Banners.Queries.GetBannersByBankId
{
    public class GetBannersByBankIdQueryHandler : IRequestHandler<GetBannersByBankIdQuery, List<GetBannersByBankIdVm>>
    {
        private readonly IBannerRepository _bannerRepository;
        public IMapper _mapper;
        public GetBannersByBankIdQueryHandler(IMapper mapper, IBannerRepository bannerRepository)
        {
            _bannerRepository = bannerRepository;
            _mapper = mapper;
        }
        public async Task<List<GetBannersByBankIdVm>> Handle(GetBannersByBankIdQuery request, CancellationToken cancellationToken)
        {
            int id = request.BankId;
            var banners = await _bannerRepository.FindBannersByBankId(id);
            var mapobject = _mapper.Map<List<GetBannersByBankIdVm>>(banners);
            return (mapobject);

        }
    }
   
}
