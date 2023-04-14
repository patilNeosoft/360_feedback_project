using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.RolePermission.Query.GetPermissionsByRole
{
    public class GetPermissionByRoleQueryVM
    {
        public int RoleId { get; set; }
        public List<string> PermissionName
        {
            get; set;
        }
    }
}
