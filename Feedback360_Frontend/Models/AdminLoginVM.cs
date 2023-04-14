using System.ComponentModel.DataAnnotations;

namespace Feedback360_Frontend.Models
{
    public class AdminLoginVM
    {
        [EmailAddress]
        [StringLength(maximumLength: 50, ErrorMessage = "EmailId is larger than maximum length ")]
        [Required(ErrorMessage = "Please Enter Email")]
        public string Email { get; set; }
        [StringLength(maximumLength: 50, ErrorMessage = "Password is larger than maximum length ")]
        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }
    }
}
