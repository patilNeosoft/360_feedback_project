using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Domain.Entities
{
    public class RolePermissionMapping
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        [ForeignKey("RoleId")]
       public virtual UserRole UserRole { get; set; }
        [ForeignKey("PermissionId")]
        public virtual RolePermission RolePermission { get; set; }
    }
}
