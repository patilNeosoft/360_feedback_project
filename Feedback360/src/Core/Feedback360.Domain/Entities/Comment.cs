using Feedback360.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedback360.Domain.Entities
{
    public class Comment:AuditableEntity
    {

        public int CommentId { get; set; }
        public string CommentText { get; set; }

        public int QueryId { get; set; }
        [ForeignKey("QueryId")]
        public virtual Query? Query
        { get; set; }

        public string RoleName { get; set; }
       
        

    }
}
