using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.SelfFeedBack.Query.GetUserFeedbackDataByFinancialYear
{
    public class GetUserFeedbackDataByFinancialYearQuery : IRequest<List<GetUserFeedbackDataByFinancialYearVm>>
    {
        public int UserId { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }    
    }

}
