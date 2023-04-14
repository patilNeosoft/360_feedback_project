using Feedback360.Application.Features.UserRoles.Queries.GetUserRoleById;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.Announcements.Queries.GetAnnouncementById
{
    public class GetAnnouncementByIdQuery : IRequest<GetAnnouncementByIdVm>
    {
        public int AnnouncementId { get; set; }
    }
}
