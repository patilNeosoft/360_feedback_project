using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.SelfFeedBack.Query.GetUserFeedbackDataByFinancialYear
{
    public class GetUserFeedbackDataByFinancialYearVm
    {
        public int FeedbackId { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string Question { get; set; }
        public string? SubjectDescription { get; set; }
        public int? SelfScore { get; set; }
        public string? SelfComment { get; set; }
    }
}
