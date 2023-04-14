using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.Banners.Queries.GetBannersByBankId
{
    public class GetBannersByBankIdQuery: IRequest<List<GetBannersByBankIdVm>>
    {
        public int BankId { get; set; }
    }
}
