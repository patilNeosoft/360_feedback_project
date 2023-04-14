namespace Feedback360_Frontend.Models
{
    public class PostSelfFeedbackFormVM
    {
        public int FeedbackId { get; set; }
        public int UserId { get; set; }
        public int BankId { get; set; }
        public int QuestionId { get; set; }
        public string? SubjectDescription { get; set; }
        public int? SelfScore { get; set; }
        public string? SelfComment { get; set; }
        public DateTime? SelfCommentDate { get; set; }
        public bool? FeedbackStatus { get; set; }
        public int? ApprovedStatus { get; set; }
    }
}
