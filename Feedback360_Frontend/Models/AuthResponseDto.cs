namespace Feedback360_Frontend.Models
{
    public class AuthResponseDto
    {
        public int UserID { get; set; }
        public string Token { get; set; }
        public string RoleName { get; set; }

        public int RoleId { get; set; }
        public string UserName { get; set; }
        public int BankId { get; set; }
        public string Email { get; set; }
    }
}
