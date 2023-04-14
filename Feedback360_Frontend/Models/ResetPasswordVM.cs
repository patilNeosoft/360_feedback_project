using System.ComponentModel.DataAnnotations;

namespace Feedback360_Frontend.Models
{
    public class ResetPasswordVM
    {
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter the new Password")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,32}$", ErrorMessage = "The password must contain 8 characters At least 1 number, 1 Uppercase letter, 1 Special Symbol")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please Enter the Confirm Password")]
        [Compare("NewPassword", ErrorMessage = "Confirm Password Does not match with New Password")]
        public string ConfirmPassword { get; set; }
    }
}
