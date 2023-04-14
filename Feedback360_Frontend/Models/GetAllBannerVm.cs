namespace Feedback360_Frontend.Models
{
    public class GetAllBannerVm
    {
        public int BannerId { get; set; }
        public string BannerTitle { get; set; }
        public string? BannerImageName { get; set; }
        public string? BannerImageUrl { get; set; }
        public virtual BankViewModel? Bank { get; set; }
        public IFormFile BannerImage { get; set; }
    }
}
