using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.SelfFeedBack.Query.GetUserAuthorityDataByUserId
{
    public class GetUserAuthorityDataByUserIdQueryVM
    {
        public int financialYear { get; set; }
        public string PrimaryRole { get; set; }
        public string Organization { get; set; }
        public string ReportingAuthority { get; set; }
        public string ReviewingAuthority { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }


    }
}
