using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Domain.Entities
{
    public class TLDetailsVm
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeId { get; set; }
        public int BankId { get; set; }
        public int RoleId { get; set; }
        public string ContactNumber { get; set; }
        public DateTime TeamJoiningDate { get; set; }
    }
}
