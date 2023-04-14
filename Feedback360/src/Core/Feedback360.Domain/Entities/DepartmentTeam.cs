using Feedback360.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Domain.Entities
{
    public class DepartmentTeam:AuditableEntity
    {
        public int DeptTeamId { get; set; }
        public int TeamLeadId { get; set; }
        public int UserId { get; set; }
        public int DeptId { get; set; }
        public int BankId { get; set; }
        public int SRoleId { get; set; }
        public DateTime? TeamJoiningDate { get; set; }

        [ForeignKey("SRoleId")]
        public virtual SecondaryRole? SecondaryRole { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
        public bool IsDeleted { get; set; }

    }

}
