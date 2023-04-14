namespace Feedback360_Frontend.Models
{
    public class RolePermissionVM
    {
        public int RoleId { get; set; }

        public List<int> PermissionId
        {
            get; set;
        }
    }
}
