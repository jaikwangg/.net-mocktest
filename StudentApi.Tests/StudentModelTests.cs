using FluentAssertions;
using StudentApi.Models;
using Xunit;

namespace StudentApi.Tests
{
    public class StudentModelTests
    {
        [Theory]
        [InlineData(80, "A")]
        [InlineData(75, "B")]
        [InlineData(65, "C")]
        [InlineData(55, "D")]
        [InlineData(40, "F")]
        public void Student_Grade_ShouldBeCalculatedCorrectly(double score, string expectedGrade)
        {
            // Arrange
            var student = new Student { Score = score };

            // Act
            var grade = student.Grade;

            // Assert
            grade.Should().Be(expectedGrade);
        }
    }
}