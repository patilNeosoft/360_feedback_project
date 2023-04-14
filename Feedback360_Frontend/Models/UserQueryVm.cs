using System.ComponentModel.DataAnnotations;

namespace Feedback360_Frontend.Models
{
    public class UserQueryVm
    {
        public int QueryId { get; set; }
        [Required]
        //[StringLength(maximumLength: 2, ErrorMessage = "Banner title should be between 2 to 50 characters.", MinimumLength = 50)]

        public string QueryTitle { get; set; }
        [Required]
        //[StringLength(maximumLength: 2, ErrorMessage = "Banner title should be between 2 to 200 characters.", MinimumLength = 200)]

        public string Description { get; set; }
        public bool QueryStatus { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }

    }
}
