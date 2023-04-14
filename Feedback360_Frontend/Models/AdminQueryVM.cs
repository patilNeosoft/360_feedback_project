namespace Feedback360_Frontend.Models
{
    public class AdminQueryVM
    {
        public int QueryId { get; set; }
        public string QueryTitle { get; set; }
        public string Description { get; set; }
        public bool QueryStatus { get; set; }
        public bool IsDeleted { get; set; }
        public int UserId { get; set; }
    }
}
