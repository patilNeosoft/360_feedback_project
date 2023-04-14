using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.UserAuthority.Commands.AddAuthority
{
    public class AddAuthorityDto
    {
        public int UserId { get; set; }
        public List<int> ReportingAuthority { get; set; }
        public List<int> ReviewingAuthority { get; set; }
    }
}
