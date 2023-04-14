using Feedback360.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Domain.Entities
{
    public class RolePermission:AuditableEntity
    {
        
        public int PermissionId { get; set; }
        public string PermissionDescription { get; set; }
        public virtual ICollection<RolePermissionMapping> RolePermissionMapping { get; set; }
    }
}
