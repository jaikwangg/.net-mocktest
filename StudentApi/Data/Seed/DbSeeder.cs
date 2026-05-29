using StudentApi.Models;

namespace StudentApi.Data.Seed
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            // 1. Seed Departments
            if (!context.Departments.Any())
            {
                context.Departments.AddRange(
                    new Department { Name = "Computer Science" },
                    new Department { Name = "Engineering" },
                    new Department { Name = "Business Administration" }
                );
            }

            // 2. Seed Courses
            if (!context.Courses.Any())
            {
                context.Courses.AddRange(
                    new Course { Title = "Introduction to Programming", Description = "Learn C# basics", Credits = 3 },
                    new Course { Title = "Database Systems", Description = "SQL and NoSQL", Credits = 4 },
                    new Course { Title = "Web Development", Description = "Full-stack development", Credits = 3 }
                );
            }

            // 3. Seed Students
            if (!context.Students.Any())
            {
                context.Students.AddRange(
                    new Student { Name = "Somsak Rakdee", Score = 85 }, // Grade A
                    new Student { Name = "Wichai Chuenjai", Score = 72 }, // Grade B
                    new Student { Name = "Manee Meena", Score = 64 }, // Grade C
                    new Student { Name = "Piti Suksan", Score = 58 }, // Grade D
                    new Student { Name = "Chujai Maimeur", Score = 45 }  // Grade F
                );
            }

            // 4. Seed Users (for Auth)
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User { Username = "admin", PasswordHash = "Admin123!", Role = "Admin" },
                    new User { Username = "user", PasswordHash = "User123!", Role = "User" }
                );
            }

            context.SaveChanges();
        }
    }
}