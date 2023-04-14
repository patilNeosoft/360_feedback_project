using Feedback360.Application.Features.Announcements.Commands.DeleteAnnouncement;
using Feedback360.Application.Features.Announcements.Commands.UpdateAnnouncement;
using Feedback360.Application.Features.Announcements.Queries.GetAllAnnouncements;
using Feedback360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Contracts.Persistence
{
    public interface IAnnouncementsRepository:IAsyncRepository<Announcements>
    {
        public Task<Announcements> FindAnnouncementById(int announcementId);
        public Task<IReadOnlyList<Announcements>> ListAllAnnouncements();
        public Task<Announcements> CreateAnnouncement(Announcements request);
        public Task<UpdateAnnouncementDto> UpdateAnnouncement(long AnnouncementId, UpdateAnnouncementCommand request);
        public Task DeleteAnnouncementAsync(Announcements announcements);
        public Task<Announcements> FindDashboardAnnouncementById(int bankId);
    }
}
