using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Feedback360_Frontend.Models
{
    public class BannerVm
    {
        public int BannerId { get; set; }

        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Banner title should be between 2 to 50 characters.", MinimumLength = 2)]
        public string BannerTitle { get; set; }

        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Banner Image Name should be between 2 to 50 characters.", MinimumLength = 2)]

        public string? BannerImageName { get; set; }
       
        public string? BannerImageUrl { get; set; }

        [Required]
        public int BankId { get; set; }

        public IFormFile? BannerImage { get; set; }
        
    }
}
