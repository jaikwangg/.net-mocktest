namespace StudentApi.Dtos
{
    public class StudentReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public double Score { get; set; }
        public string Grade { get; set; } = "";
    }

    public class StudentCreateDto
    {
        public string Name { get; set; } = "";
        public double Score { get; set; }
    }

    public class StudentUpdateDto
    {
        public string Name { get; set; } = "";
        public double Score { get; set; }
    }
}namespace StudentApi.Dtos
{
    public class LoginDto
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class UserDto
    {
        public string Username { get; set; } = "";
        public string Token { get; set; } = "";
    }
}