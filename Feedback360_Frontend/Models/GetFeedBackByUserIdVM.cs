namespace Feedback360_Frontend.Models
{
    public class GetFeedBackByUserIdVM
    {
        public int FeedbackId { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string Question { get; set; }
        public string? SubjectDescription { get; set; }
        public int? SelfScore { get; set; }
        public string? SelfComment { get; set; }
    }
}
