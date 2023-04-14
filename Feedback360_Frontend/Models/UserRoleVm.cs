using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Feedback360_Frontend.Models
{
    public class UserRoleVm
    {
        public int RoleId { get; set; }
        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Role Name should be between 2 to 50 characters.", MinimumLength = 2)]
        public string RoleName { get; set; }
       
        public bool IsActive { get; set; } 
        public bool IsDeleted { get; set; } 
    }
}
