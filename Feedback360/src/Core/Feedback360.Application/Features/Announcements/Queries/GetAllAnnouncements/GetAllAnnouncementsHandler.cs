using AutoMapper;
using Feedback360.Application.Contracts.Persistence;
using Feedback360.Application.Features.Banners.Queries.GetAllBanners;
using Feedback360.Application.Features.UserRoles.Queries.GetAllUserRoles;
using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.Announcements.Queries.GetAllAnnouncements
{
    public class GetAllAnnouncementsHandler:IRequestHandler<GetAllAnnouncementsQuery, IEnumerable<GetAllAnnouncementsVm>>
    {
        private readonly IAnnouncementsRepository _announcementsRepository;
    public IMapper _mapper;
    public GetAllAnnouncementsHandler(IMapper mapper, IAnnouncementsRepository announcementsRepository)
    {
        _announcementsRepository = announcementsRepository;
        _mapper = mapper;
    }

        public async Task<IEnumerable<GetAllAnnouncementsVm>> Handle(GetAllAnnouncementsQuery request, CancellationToken cancellationToken)
        {
            var allAnnouncements = await _announcementsRepository.ListAllAnnouncements();
            IEnumerable<GetAllAnnouncementsVm> getAllAnnouncementsVms = _mapper.Map<IEnumerable<GetAllAnnouncementsVm>>(allAnnouncements);
            return getAllAnnouncementsVms;
        }
    }
}
