using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.UserQueries.Command.CreateUserQuery
{
    public class CreateUserQueryDto
    {
        public int QueryId { get; set; }
        public string QueryTitle { get; set; }
        public string Description { get; set; }
        public bool QueryStatus { get; set; }
        
        
    }
}
