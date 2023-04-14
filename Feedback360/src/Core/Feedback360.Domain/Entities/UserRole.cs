﻿using Feedback360.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Domain.Entities
{
    public class UserRole:AuditableEntity
    {

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<RolePermissionMapping> RolePermissionMapping { get; set; }

    }
}
