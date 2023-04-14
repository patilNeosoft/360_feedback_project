using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Domain.Entities
{
    public class FinancialYear
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public bool? IsActive { get; set; } = true;
        public int? AddedBy { get; set; }
        public DateTime? AddedOn { get; set; } = DateTime.UtcNow;
        public bool? IsDeleted { get; set; } = false;
        public DateTime? DeletedOn { get; set; } = DateTime.UtcNow;
        public int? DeletedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; } = DateTime.UtcNow;

    }

}
