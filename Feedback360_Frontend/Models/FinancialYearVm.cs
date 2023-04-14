namespace Feedback360_Frontend.Models
{
    public class FinancialYearVm
    {
        public int Id { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
