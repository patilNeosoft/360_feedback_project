using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Domain.Entities
{
    public class UserFeedbackDetailsVm
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
