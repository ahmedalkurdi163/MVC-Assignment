namespace MVC_App.Models
{
    public class AspNetUserRole
    {
        public string UserId { get; set; }
        public AspNetUser User { get; set; }

        public string RoleId { get; set; }
        public AspNetRole Role { get; set; }
    }
}
