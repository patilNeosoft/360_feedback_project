namespace Feedback360_Frontend.Models
{
    public class GetUsersByBankIdVM
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeId { get; set; }
        public int? ReviewingAuthority { get; set; }
        public int? ReportingAuthority { get; set; }
        public string? ReportingAuthority_FirstName { get; set; }
        public string? ReportingAuthority_LastName { get; set; }
        public string? ReviewingAuthority_FirstName { get; set; }
        public string? ReviewingAuthority_LastName { get; set; }
    }
}
