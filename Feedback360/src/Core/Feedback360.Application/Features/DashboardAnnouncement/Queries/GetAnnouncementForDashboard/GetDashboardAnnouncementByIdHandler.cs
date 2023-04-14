using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.Announcements.Queries.GetAnnouncementById;
using Feedback360.Application.Features.UserRoles.Queries.GetUserRoleById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.DashboardAnnouncement.Queries.GetAnnouncementForDashboard
{
    public class GetDashboardAnnouncementByIdHandler : IRequestHandler<GetDashboardAnnouncementByIdQuery, GetDashboardAnnouncementByIdVm>
    {
        private readonly IAnnouncementsRepository _announcementRepository;
        public IMapper _mapper;
        public GetDashboardAnnouncementByIdHandler(IMapper mapper, IAnnouncementsRepository announcementRepository)
        {
            _announcementRepository = announcementRepository;
            _mapper = mapper;
        }

        public async Task<GetDashboardAnnouncementByIdVm> Handle(GetDashboardAnnouncementByIdQuery request, CancellationToken cancellationToken)
        {
            int bankId = request.BankId;
            var dashboardAnnouncement = await _announcementRepository.FindDashboardAnnouncementById(bankId);
            var mapobject = _mapper.Map<GetDashboardAnnouncementByIdVm>(dashboardAnnouncement);
            return (mapobject);
        }
    }
}
