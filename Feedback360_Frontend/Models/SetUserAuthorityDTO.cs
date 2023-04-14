namespace Feedback360_Frontend.Models
{
    public class SetUserAuthorityDTO
    {
        public int UserId { get; set; }
        public int? ReportingAuthority { get; set; }
        public int? ReviewingAuthority { get; set; }
    }
}
