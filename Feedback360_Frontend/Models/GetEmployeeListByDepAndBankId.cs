namespace Feedback360_Frontend.Models
{
    public class GetEmployeeListByDepAndBankId
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public string Department { get; set; }
        public string Bank { get; set; }
    }
}
