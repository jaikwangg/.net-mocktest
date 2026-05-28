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
}