using Feedback360.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Domain.Entities
{
    public class User:AuditableEntity
    {
            
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string EmployeeId { get; set; }
        public string Password { get; set; }
        public int BankId { get; set; }
        public int RoleId { get; set; }
        public string ContactNumber { get; set; }
        public string Organization { get; set; }
        [DataType(DataType.Date)]

        public int? DeptId { get; set; }
        
        [ForeignKey("DeptId")]
        public virtual Department? Department { get; set; }

        public DateTime JoiningDate { get; set; }
        [ForeignKey("RoleId")]
        public virtual UserRole? UserRole { get; set; }
        [ForeignKey("BankId")]
        public virtual Bank? Bank { get; set; }
    }
}
