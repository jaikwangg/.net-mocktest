namespace StudentApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public string PasswordHash { get; set; } = ""; // ในระบบจริงต้องเก็บเป็น Hash
        public string Role { get; set; } = "User"; // e.g., Admin, User
    }
}