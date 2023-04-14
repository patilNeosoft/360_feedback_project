using Feedback360.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Domain.Entities
{
    public class Questionnaire : AuditableEntity
    {
        public int QuestionId { get; set; }
        public string Question { get; set; }
        public bool IsActive { get; set; }

        public int BankId { get; set; }

        [ForeignKey("BankId")]
        public virtual Bank? Bank { get; set; }
    }

}
