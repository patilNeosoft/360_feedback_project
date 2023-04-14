using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Domain.Entities
{
    public class Banner
    {
        public int BannerId { get; set; }
        public string BannerTitle { get; set; }
        public string? BannerImageName { get; set; }
        public string? BannerImageUrl { get; set; }
        public int BankId { get; set; }

        [ForeignKey("BankId")]
        public virtual Bank? Bank { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
