using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.SelfFeedBack.Query.GetSelfFeedbackSummary
{
    public class GetSelfFeedbackSummaryQuery: IRequest<List<GetSelfFeedbackSummaryVm>>
    {
        public int UserId { get; set; }
        public int FinancialYear { get; set; }
        
    }
   
}
