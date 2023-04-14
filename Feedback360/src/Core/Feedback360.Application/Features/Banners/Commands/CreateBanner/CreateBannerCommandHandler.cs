using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Responses;
using Feedback360.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.Banners.Commands.CreateBanner
{
    public class CreateBannerCommandHandler: IRequestHandler<CreateBannerCommand, Response<CreateBannerDto>>
    {
        private readonly IBannerRepository _bannerRepository;
        public IMapper _mapper;
        public CreateBannerCommandHandler(IMapper mapper, IBannerRepository bannerRepository)
        {
            _bannerRepository = bannerRepository;
            _mapper = mapper;
        }

        public async Task<Response<CreateBannerDto>> Handle(CreateBannerCommand request, CancellationToken cancellationToken)
        {
            var bannerToAdd = _mapper.Map<Banner>(request);
            var bannerAdded = await _bannerRepository.AddAsync(bannerToAdd);
            return new Response<CreateBannerDto>(_mapper.Map<CreateBannerDto>(bannerAdded));
          }
    }
   
}
