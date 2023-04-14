using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.UserQueries.Queries.GetUserQueries
{
    public class GetUserQueriesQueryVm
    {
        public int QueryId { get; set; }
        public string QueryTitle { get; set; }
        public string Description { get; set; }
        public bool QueryStatus { get; set; }
       
    }
}
