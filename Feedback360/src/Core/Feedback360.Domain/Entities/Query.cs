using Feedback360.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feedback360.Domain.Common;

namespace Feedback360.Domain.Entities
{
    public class Query:AuditableEntity
    {
        [Key]
        public int QueryId { get; set; }
        public string QueryTitle { get; set; }
        public string Description { get; set; }
        public bool QueryStatus { get; set; }
     
        public bool IsDeleted { get; set; } = false;
      

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }



    }
}
