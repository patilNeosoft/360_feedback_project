namespace Feedback360_Frontend.Models
{
    public class GetMyTeamVm
    {
        public int DeptTeamId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string SRole { get; set; }
        public DateTime? TeamJoiningDate { get; set; }
    }
}
