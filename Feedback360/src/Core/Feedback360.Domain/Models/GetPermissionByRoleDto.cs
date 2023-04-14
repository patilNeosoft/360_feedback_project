using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Domain.Models
{
    public class GetPermissionByRoleDto
    {
        public int RoleId { get; set; }
        public List<int> PermissionName
        {
            get; set;
        }
    }
}
