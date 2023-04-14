using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Domain.Models
{
    public class PresentUserFeedbacks
    {
      
        public int UserId { get; set; }
        public int UserFeedbackId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeId { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
    }
}
