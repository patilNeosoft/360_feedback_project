using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Application.Features.UserAuthority.Queries.GetReviewingAuthorityByBankId
{
    public class GetReviewingAuthorityByBankIdVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int BankId { get; set; }
        public int RoleId { get; set; }
    }
}
