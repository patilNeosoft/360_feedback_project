using System.ComponentModel.DataAnnotations;

namespace Feedback360_Frontend.Models
{
    public class DepartmentTeamVm
    {
        public int TeamLeadId { get; set; }
        public int UserId { get; set; }
       // public string Name { get; set; }
        public int DeptId { get; set; }
        public int BankId { get; set; }
        [Required(ErrorMessage ="select secondary role")]
        public int SRoleId { get; set; }
        
    }
}
