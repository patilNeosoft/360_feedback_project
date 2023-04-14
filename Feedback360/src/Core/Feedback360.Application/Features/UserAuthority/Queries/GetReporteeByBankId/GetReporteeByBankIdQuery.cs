using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.UserAuthority.Queries.GetReporteeByBankId
{
    public class GetReporteeByBankIdQuery : IRequest<List<GetReporteeByBankIdVM>>
    {
        public int BankId { get; set; }
    }
}
