using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Persistence.Models
{
    public class UserLoginVM
    {
        public string EmployeeId { get; set; }
        public string Password { get; set; }
        public int BankId { get; set; }
        
        public string CaptchaCode { get; set; }
    }
}
