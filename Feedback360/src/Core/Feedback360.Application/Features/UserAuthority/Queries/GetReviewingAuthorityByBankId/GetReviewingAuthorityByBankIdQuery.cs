using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.UserAuthority.Queries.GetReviewingAuthorityByBankId
{
    public class GetReviewingAuthorityByBankIdQuery : IRequest<List<GetReviewingAuthorityByBankIdVM>>
    {
        public int BankId { get; set; }
    }
}
