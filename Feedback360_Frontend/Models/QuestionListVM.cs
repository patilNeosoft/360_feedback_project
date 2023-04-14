using System.ComponentModel.DataAnnotations;

namespace Feedback360_Frontend.Models
{
    public class QuestionListVM
    {
        public int QuestionId { get; set; }
        public string Question { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(maximumLength: 300, ErrorMessage = "Description must have atleast 3 charcters. ", MinimumLength = 3)]
        public string SubjectDescription { get; set; }
        [Required(ErrorMessage = "Self Score is required.")]
        public int SelfScore { get; set; }
        [Required(ErrorMessage = "Self Comment is required.")]
        [StringLength(maximumLength: 200, ErrorMessage = "Comment must have atleast 3 charcters. ", MinimumLength = 3)]
        public string SelfComment { get; set; }
    }
}
