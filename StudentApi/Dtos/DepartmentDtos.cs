namespace StudentApi.Dtos
{
    public class DepartmentReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
    }

    public class DepartmentCreateDto
    {
        public string Name { get; set; } = "";
    }
}