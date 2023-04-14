using System.ComponentModel.DataAnnotations;

namespace Feedback360_Frontend.Models
{
    public class RevaFeedbackUserFormUIVM
    {
        public int FeedbackId { get; set; }
        public int UserId { get; set; }
        public int BankId { get; set; }
        public int QuestionId { get; set; }
        public string Question { get; set; }
        public string? SubjectDescription { get; set; }
        public int? SelfScore { get; set; }
        public string? SelfComment { get; set; }
        public DateTime? SelfCommentDate { get; set; } 
        public int? RepaScore { get; set; }
        public string? RepaComment { get; set; }
        public DateTime? RepaCommentDate { get; set; }
        [Required(ErrorMessage = "Reva Score is required.")]
        public int? RevaScore { get; set; }
        [Required(ErrorMessage = "Reva Comment is required.")]
        [StringLength(maximumLength: 200, ErrorMessage = "Comment must have atleast 3 charcters. ", MinimumLength = 3)]
        public string? RevaComment { get; set; }
        public DateTime? RevaCommentDate { get; set; }
        public bool? FeedbackStatus { get; set; }
        public int? ApprovedStatus { get; set; }
    }
}
