using System;
using System.Collections.Generic;
using HelloWorld;

Console.WriteLine("=== ระบบจัดการข้อมูลนักเรียน (OOP Basics) ===");

Student s1 = new Student("สมชาย", 85.5);
Student s2 = new Student("สมหญิง", 62.0);

s1.DisplayInfo();
s2.DisplayInfo();

List<Student> students = new List<Student>();
students.Add(s1);
students.Add(s2);
students.Add(new Student("วิชัย", 45.0));

Console.WriteLine("\n--- รายชื่อนักเรียนทั้งหมดในระบบ ---");
foreach (var s in students)
{
    s.DisplayInfo();
}

Console.WriteLine("\nลองแก้ไขคะแนนของสมหญิง...");
s2.Score = 75.0;
s2.DisplayInfo();

Console.WriteLine("\nจบการสาธิตเรื่อง Class และ Object");