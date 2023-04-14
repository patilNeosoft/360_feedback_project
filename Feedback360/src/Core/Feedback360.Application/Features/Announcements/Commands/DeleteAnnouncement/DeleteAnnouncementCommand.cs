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
    public class DeleteAnnouncementCommand : IRequest<DeleteAnnouncementDto>
    {
        public int AnnouncementId { get; set; }
    }
}
