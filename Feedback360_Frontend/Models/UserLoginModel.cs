using System.ComponentModel.DataAnnotations;

namespace Feedback360_Frontend.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Employee Id is required.")]
        [StringLength(maximumLength: 10, ErrorMessage = "EmployeeId must have atleast 2 charcters. ", MinimumLength = 2)]
     
        public string EmployeeId { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(.{8,50})$",
        ErrorMessage = "Password must contain a Capital letter and a Number within range 8 to 50 ")]
        [StringLength(maximumLength: 50, ErrorMessage = "Password is larger than maximum length ")]
    
        public string Password { get; set; }
        [Required(ErrorMessage = "Bank Name is required.")]
        public int BankId { get; set; }

        [Required]
        [StringLength(4)]
        public string CaptchaCode { get; set; }
    }
}
