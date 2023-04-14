using Feedback360.Application.Features.Banners.Queries.GetBannersByBankId;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.FeedbackForm.Queries.GetQuestionsByBankId
{
    public class GetQuestionsByBankIdQuery: IRequest<List<GetQuestionsByBankIdQueryVM>>
    {
        public int BankId { get; set; }
    }
}
