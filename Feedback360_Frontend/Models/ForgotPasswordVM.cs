using System.ComponentModel.DataAnnotations;

namespace Feedback360_Frontend.Models
{
    public class ForgotPasswordVM
    {
        [Required(ErrorMessage = "Please Enter the Email Field")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please Enter Valid Email!!")]
        [StringLength(maximumLength: 50, ErrorMessage = "Email must have atleast 5 charcters. ", MinimumLength = 5)]
        public string Email { get; set; }
    }
}
