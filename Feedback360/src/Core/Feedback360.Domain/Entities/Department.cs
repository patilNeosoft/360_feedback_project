using Feedback360.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Domain.Entities
{
    public class Department : AuditableEntity
    {
        public int DeptId { get; set; }
        public string DeptName { get; set; }

    }

}
