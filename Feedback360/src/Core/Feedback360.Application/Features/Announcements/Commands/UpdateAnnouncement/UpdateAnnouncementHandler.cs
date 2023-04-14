using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.Banners.Commands.UpdateBanner;
using Feedback360.Application.Features.UserRoles.Commands.UpdateUserRole;
using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.Announcements.Commands.UpdateAnnouncement
{
    public class UpdateAnnouncementHandler : IRequestHandler<UpdateAnnouncementCommand, UpdateAnnouncementDto>
    {
        private readonly IAnnouncementsRepository _announcmentsRepository;
        public IMapper _mapper;
        public UpdateAnnouncementHandler(IMapper mapper, IAnnouncementsRepository announcmentsRepository)
        {
            _announcmentsRepository = announcmentsRepository;
            _mapper = mapper;
        }
    
        public async Task<UpdateAnnouncementDto> Handle(UpdateAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var announcementDto = await _announcmentsRepository.UpdateAnnouncement(request.AnnouncementId, request);

            if (announcementDto.Succeeded)
            {
                var updatedAnnouncement = _mapper.Map<UpdateAnnouncementDto>(announcementDto);
                return (updatedAnnouncement);
            }
            else {
                var res = _mapper.Map<UpdateAnnouncementDto>(announcementDto);
                res.Succeeded = false;
                return res;
            }
        }
    }
}
