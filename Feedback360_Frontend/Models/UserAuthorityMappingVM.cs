namespace Feedback360_Frontend.Models
{
    public class UserAuthorityMappingVM
    {
        public int FinancialYear { get; set; }
        public int UserId { get; set; }
        public string PrimaryRole { get; set; }
        public string Organization { get; set; }
        public string ReportingAuthority { get; set; }
        public string ReviewingAuthority { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
    }
}
