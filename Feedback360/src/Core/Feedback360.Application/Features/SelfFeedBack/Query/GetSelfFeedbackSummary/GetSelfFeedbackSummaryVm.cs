using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.SelfFeedBack.Query.GetSelfFeedbackSummary
{
    public class GetSelfFeedbackSummaryVm
    {
        public string UserName { get; set; }
        public string EmployeeId { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public bool Status { get; set; }
    }
}
