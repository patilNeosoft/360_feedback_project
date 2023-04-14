using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.Banners.Queries.GetBannerById
{
    public class GetBannerByIdQuery:IRequest<GetBannerByIdVm>
    {
        public int BannerId { get; set; }
    }
}
