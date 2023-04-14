using Feedback360.Application.Features.Announcements.Queries.GetAnnouncementById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.DashboardAnnouncement.Queries.GetAnnouncementForDashboard
{
    public class GetDashboardAnnouncementByIdQuery : IRequest<GetDashboardAnnouncementByIdVm>
    {
        public int BankId { get; set; }
    }
}
