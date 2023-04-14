using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Domain.Entities
{
    public class FeedbackAnswerForm
    {

        public int Id { get; set; }
        public int FeedbackId { get; set; }
        public int UserId { get; set; }
        public int BankId { get; set; }
        public int QuestionId { get; set; }
        public string? SubjectDescription { get; set; }
        public int? SelfScore { get; set; }
        public string? SelfComment { get; set; }
        public DateTime? SelfCommentDate { get; set; } = DateTime.Now;
        public int? RepaScore { get; set; }
        public string? RepaComment { get; set; }
        public DateTime? RepaCommentDate { get; set; }
        public int? RevaScore { get; set; }
        public string? RevaComment { get; set; }
        public DateTime? RevaCommentDate { get; set; }
        public bool? FeedbackStatus { get; set; } = false;

        public int? ApprovedStatus { get; set; }


        [ForeignKey("QuestionId")]
        public virtual Questionnaire? Questionnaire { get; set; }    

    }
}
