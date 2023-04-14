using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Domain.Entities
{
    public class UserAuthorityMappingVm
    {
        public int FinancialYear { get; set; }
        public int UserId { get; set; }
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
