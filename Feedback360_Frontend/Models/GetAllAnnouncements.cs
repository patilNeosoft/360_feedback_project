using Feedback360.Domain.Entities;

namespace Feedback360_Frontend.Models
{
    public class GetAllAnnouncements
    {
        public int AnnouncementId { get; set; }
        public string Message { get; set; }
        public bool IsActive { get; set; }
        public int BankId { get; set; }
        public virtual BankViewModel? Bank { get; set; }
    }
}
