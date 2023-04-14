using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.UserRoles.Queries.GetUserRoleById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.Announcements.Queries.GetAnnouncementById
{
    public class GetAnnouncementByIdHandler : IRequestHandler<GetAnnouncementByIdQuery, GetAnnouncementByIdVm>
    {
        private readonly IAnnouncementsRepository _announcementRepository;
        public IMapper _mapper;
        public GetAnnouncementByIdHandler(IMapper mapper, IAnnouncementsRepository announcementRepository)
        {
            _announcementRepository = announcementRepository;
            _mapper = mapper;
        }

        public async Task<GetAnnouncementByIdVm> Handle(GetAnnouncementByIdQuery request, CancellationToken cancellationToken)
        {
            
                int announcementId = request.AnnouncementId;
                var announcement = await _announcementRepository.FindAnnouncementById(announcementId);
                var result = _mapper.Map<GetAnnouncementByIdVm>(announcement);
                return (result);
        }
    }
}
