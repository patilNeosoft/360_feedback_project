using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Exceptions;
using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.Banners.Commands.DeleteBanner
{
    public class DeleteBannerCommandHandler: IRequestHandler<DeleteBannerCommand, DeleteBannerDto>
    {
        private readonly IBannerRepository _bannerRepository;
        public IMapper _mapper;
        public DeleteBannerCommandHandler(IMapper mapper, IBannerRepository bannerRepository)
        {
            _bannerRepository = bannerRepository;
            _mapper = mapper;
        }
        public async Task<DeleteBannerDto> Handle(DeleteBannerCommand request, CancellationToken cancellationToken)
        {
            int bannerIdToDelete = request.BannerId;
            var bannerToDelete = await _bannerRepository.FindBannerById(bannerIdToDelete);
            if (bannerToDelete == null)
            {
                throw new NotFoundException(nameof(bannerToDelete), bannerIdToDelete);

            }
            else
            {
                await _bannerRepository.RemoveBannerAsync(bannerToDelete);
                DeleteBannerDto deletedUserRoleName = _mapper.Map<DeleteBannerDto>(bannerToDelete);
                return (deletedUserRoleName);
            }

        }
    } 
    
}

