using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.Banners.Commands.DeleteBanner
{
    public class DeleteBannerCommand:IRequest<DeleteBannerDto>
    {
        public int BannerId { get; set; }
    }
}
