using Feedback360.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Domain.Entities
{
    public class SecondaryRole : AuditableEntity
    {
        public int SRoleID { get; set; }
        public string SRoleName { get; set; }
    }
}
