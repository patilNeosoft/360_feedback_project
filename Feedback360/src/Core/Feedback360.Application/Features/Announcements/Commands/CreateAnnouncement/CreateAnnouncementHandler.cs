using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.Categories.Commands.CreateCategory;
using Feedback360.Application.Features.UserRoles.Commands.CreateUserRole;
using Feedback360.Application.Responses;
using Feedback360.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.Announcements.Commands.CreateAnnouncement
{
    public class CreateAnnouncementHandler : IRequestHandler<CreateAnnouncementCommand, Response<CreateAnnouncementDto>>
    {
        private readonly IAnnouncementsRepository _announcementRepository;
        public IMapper _mapper;
        public CreateAnnouncementHandler(IMapper mapper, IAnnouncementsRepository announcementRepository)
        {
            _announcementRepository = announcementRepository;
            _mapper = mapper;
        }

        public async Task<Response<CreateAnnouncementDto>> Handle(CreateAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var announcementToAdd = _mapper.Map<Feedback360.Domain.Entities.Announcements>(request);
            var announcementAdded = await _announcementRepository.CreateAnnouncement(announcementToAdd);

            var response = _mapper.Map<CreateAnnouncementDto>(announcementAdded);

            if (announcementAdded != null)
            {
                var res = new Response<CreateAnnouncementDto>(_mapper.Map<CreateAnnouncementDto>(response), "success");
                res.Succeeded = true;
                return res;
            }
            else
            {
                var res = new Response<CreateAnnouncementDto>(_mapper.Map<CreateAnnouncementDto>(response), "failed");
                res.Succeeded = false;
                return res;
            }
        }
    }
}
