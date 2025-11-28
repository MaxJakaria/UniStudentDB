using UniStudentDB.Features.Students.Domain.Entities;

namespace UniStudentDB.Features.Students.Data.Models
{
    // Inheritance
    public class StudentModel : Student
    {
        // Entity থেকে Model এ কনভার্ট করার মেথড
        public static StudentModel FromEntity(Student student)
        {
            return new StudentModel
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Department = student.Department,
                Cgpa = student.Cgpa
            };
        }
    }
}