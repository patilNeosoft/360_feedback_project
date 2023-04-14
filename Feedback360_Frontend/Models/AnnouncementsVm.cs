using System.ComponentModel.DataAnnotations;

namespace Feedback360_Frontend.Models
{
    public class AnnouncementsVm
    {
        public int AnnouncementId { get; set; }

        [Required(ErrorMessage = "Please Enter the Announcement")]
        [StringLength(maximumLength: 200, ErrorMessage = "Announcement should be between 10 to 200 characters.", MinimumLength = 10)]

        public string Message { get; set; }
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "Please Select Bank Name")]
        public int BankId { get; set; }
        public string BankName { get; set; }
    }
}
