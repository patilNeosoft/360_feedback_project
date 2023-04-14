namespace Feedback360_Frontend.Models
{
    public class GetTeamLeaderDetailsVm
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeId { get; set; }
        public int BankId { get; set; }
        public int RoleId { get; set; }
        public string ContactNumber { get; set; }
        public DateTime TeamJoiningDate { get; set; }
    }
}
