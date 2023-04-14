using Feedback360.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Domain.Entities
{
    public class UserAuthorityMapping : AuditableEntity
    {
        public int UserAuthorityId { get; set; }
        public int UserId { get; set; }
        public int? ReportingAuthority { get; set; }
        public int? ReviewingAuthority  { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

    }
}

