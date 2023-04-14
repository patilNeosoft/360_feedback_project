using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Exceptions;
using Feedback360.Application.Features.Banners.Commands.DeleteBanner;
using Feedback360.Application.Features.UserRoles.Commands.DeleteUserRole;
using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.Announcements.Commands.DeleteAnnouncement
{
    public class DeleteAnnouncementHandler : IRequestHandler<DeleteAnnouncementCommand, DeleteAnnouncementDto>
    {
        private readonly IAnnouncementsRepository _announcementRepository;
        public IMapper _mapper;



        public DeleteAnnouncementHandler(IMapper mapper, IAnnouncementsRepository announcementRepository)
        {
            _announcementRepository = announcementRepository;
            _mapper = mapper;
        }

        public async Task<DeleteAnnouncementDto> Handle(DeleteAnnouncementCommand request, CancellationToken cancellationToken)
        {
            int announcementIdToDelete = request.AnnouncementId;
            var announcementToDelete = await _announcementRepository.FindAnnouncementById(announcementIdToDelete);
            if (announcementToDelete == null)
            {
                throw new NotFoundException(nameof(announcementToDelete), announcementToDelete);
            }
            else
            {
                await _announcementRepository.DeleteAnnouncementAsync(announcementToDelete);
                DeleteAnnouncementDto deletedAnnouncement = _mapper.Map<DeleteAnnouncementDto>(announcementToDelete);
                return (deletedAnnouncement);
            }
        }
    }
}
