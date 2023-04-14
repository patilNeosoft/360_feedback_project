﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.UserRoles.Commands.CreateUserRole
{
    public class CreateUserRoleDto
    {

        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
