using System.ComponentModel.DataAnnotations;

namespace Feedback360_Frontend.Models
{
    public class ChangePasswordVM
    {
        [Required(ErrorMessage = "Please Enter Old Password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Please Enter the new Password")]
       
        
        [Microsoft.AspNetCore.Mvc.Remote("IsOldAndNewPasswordSame","User",HttpMethod="POST", AdditionalFields = "OldPassword", ErrorMessage = "New password cannot be same as old password !")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,32}$", ErrorMessage = "The password must contain 8 characters At least 1 number, 1 Uppercase letter, 1 Special Symbol")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please Enter the Confirm Password")]
        [Compare("NewPassword", ErrorMessage = "Does not match with New Password")]
        public string ConfirmPassword { get; set; }
    }
}
