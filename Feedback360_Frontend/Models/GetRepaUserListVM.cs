namespace Feedback360_Frontend.Models
{
    public class GetRepaUserListVM
    {
        public int UserId { get; set; }
        public int UserFeedbackId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeId { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
    }
}
