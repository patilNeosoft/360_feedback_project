using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.Banners.Commands.CreateBanner
{
    public class CreateBannerCommand : IRequest<Response<CreateBannerDto>>
    {


        public string BannerTitle { get; set; }
        public string? BannerImageName { get; set; }
        public string? BannerImageUrl { get; set; }
        public int BankId { get; set; }

    }
}
