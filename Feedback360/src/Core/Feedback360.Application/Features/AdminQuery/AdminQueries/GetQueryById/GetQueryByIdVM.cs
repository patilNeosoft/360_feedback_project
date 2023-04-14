using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.AdminQuery.AdminQueries.GetQueryById
{
    public class GetQueryByIdVM
    {
        public int QueryId { get; set; }
        public string QueryTitle { get; set; }
        public string Description { get; set; }
        public bool QueryStatus { get; set; }
        public bool IsDeleted { get; set; }
        public int UserId
        {
            get; set;
        }
    }
}
