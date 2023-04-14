using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.Announcements.Commands.CreateAnnouncement
{
    public class CreateAnnouncementDto
    {
        public int AnnouncementId { get; set; }
        public string Message { get; set; }
        public bool IsActive { get; set; }
        public int BankId { get; set; }
        public bool Succeeded { get; set; }
    }
}
