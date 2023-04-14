using Feedback360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.Announcements.Queries.GetAllAnnouncements
{
    public class GetAllAnnouncementsVm
    {
        public int AnnouncementId { get; set; }
        public string Message { get; set; }
        public bool IsActive { get; set; }
        public int BankId { get; set; }
        public virtual Bank? Bank { get; set; }
    }
}
