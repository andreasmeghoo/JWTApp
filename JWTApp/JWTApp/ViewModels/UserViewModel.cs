namespace JWTApp.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = "User";

        public bool IsApproved { get; set; } = false;
    }
}
