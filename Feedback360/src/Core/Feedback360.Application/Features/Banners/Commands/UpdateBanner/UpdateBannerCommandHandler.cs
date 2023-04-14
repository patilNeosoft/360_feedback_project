using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.Banners.Commands.UpdateBanner
{
    public class UpdateBannerCommandHandler: IRequestHandler<UpdateBannerCommand, UpdateBannerDto>
    {
        private readonly IBannerRepository _bannerRepository;
        public IMapper _mapper;
        public UpdateBannerCommandHandler(IMapper mapper, IBannerRepository bannerRepository)
        {
            _bannerRepository = bannerRepository;
            _mapper = mapper;
        }
        public async Task<UpdateBannerDto> Handle(UpdateBannerCommand request, CancellationToken cancellationToken)
        {
            int bannerId = request.BannerId;
            var bannerFromDb = await _bannerRepository.FindBannerById(bannerId);

            if (bannerFromDb == null)
            {
                throw new NotFoundException(nameof(bannerFromDb), request.BannerId);
            }
            else
            {
                var bannerToUpdate = _mapper.Map(request, bannerFromDb);

                await _bannerRepository.UpdateAsync(bannerToUpdate);

                var updatedBanner = _mapper.Map<UpdateBannerDto>(bannerToUpdate);

                return (updatedBanner);
            }
        }

    }
}
