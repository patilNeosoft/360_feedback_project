using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.Announcements.Commands.UpdateAnnouncement
{
    public class UpdateAnnouncementCommand : IRequest<UpdateAnnouncementDto>
    {
        public long AnnouncementId { get; set; }
        public string Message { get; set; }
        public bool IsActive { get; set; }
        public int BankId { get; set; }
    }
}
