using System.ComponentModel.DataAnnotations;

namespace Feedback360_Frontend.Models
{
    public class OtpVM
    {
        [Required(ErrorMessage = "OTP is required.")]
        [Range(100000, 999999, ErrorMessage = "Please Enter valid OTP")]
        public int OtpCode { get; set; }
    }
}
