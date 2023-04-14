using Feedback360.Domain.Common;
using MessagePack;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Domain.Entities
{
    public class FeedbackUserMapping:AuditableEntity
    {
        
        public int UserFeedbackId { get; set; }
        public int UserId { get; set; }
        public int BankId { get; set; }
        public int FYId { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        [ForeignKey("FYId")]
        public virtual FinancialYear?FinancialYear { get; set; }


    }
}
