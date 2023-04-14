using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.UserRoles.Commands.UpdateUserRole
{
    public class UpdateUserRoleDto
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; } 
    }
}
