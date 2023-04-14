using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.Banners.Commands.DeleteBanner
{
    public class DeleteBannerDto
    {
        public int BannerId { get; set; }
        public string BannerTitle { get; set; }
        public string? BannerImageName { get; set; }
        public string? BannerImageUrl { get; set; }
        public int BankId { get; set; }
    }
}
