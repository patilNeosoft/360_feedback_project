using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.Announcements.Commands.UpdateAnnouncement
{
    public class UpdateAnnouncementDto
    {
        public long AnnouncementId { get; set; }
        public string SuccessMessage { get; set; }
        public bool Succeeded { get; set; }
    }
}
