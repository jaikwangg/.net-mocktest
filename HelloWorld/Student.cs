namespace HelloWorld
{
    public class Student
    {
        // 1. Properties (ข้อมูลของนักเรียน)
        public string Name { get; set; }
        public double Score { get; set; }

        // 2. Constructor (ตัวช่วยตอนสร้าง Object ใหม่)
        public Student(string name, double score)
        {
            Name = name;
            Score = score;
        }

        // 3. Method (ความสามารถของนักเรียน)
        public string GetGrade()
        {
            if (Score >= 80) return "A";
            if (Score >= 70) return "B";
            if (Score >= 60) return "C";
            if (Score >= 50) return "D";
            return "F";
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"นักเรียน: {Name} | คะแนน: {Score} | เกรด: {GetGrade()}");
        }
    }
}
