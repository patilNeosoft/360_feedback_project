using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.DepartmentTeams.Query.GetActiveFinancialYear
{
    public class GetActiveFinancialYearVm
    {
        public int Id { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public bool? IsActive { get; set; } 

    }
}
