using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Domain.Entities
{
    public class AuthResponse
    {
        public int UserID { get; set; }
        public string Token { get; set; }
        public string RoleName { get; set; }

        public int RoleId { get; set; }
        public string UserName { get; set; }
        public int BankId { get; set; }
        public string Email { get; set; }
    }
}
