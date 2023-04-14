using Feedback360.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.Banks.Query
{
    public class GetBankListQuery: IRequest<Response<IEnumerable<GetBankListVM>>>
    {
    }
}
