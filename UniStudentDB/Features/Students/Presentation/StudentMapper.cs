using UniStudentDB.Features.Students.Domain.Entities;
using UniStudentDB.Features.Students.Presentation.Dto;

namespace UniStudentDB.Features.Students.Presentation
{
    public static class StudentMapper
    {
        // 1. Request -> Entity
        public static Student ToEntity(this CreateStudentRequest request)
        {
            return new Student
            {
                Name = request.Name,
                Email = request.Email,
                Department = request.Department,
                Cgpa = request.Cgpa,
            };
        }

        // 2. Request (Update) -> Entity
        public static Student ToEntity(this UpdateStudentRequest request, int id)
        {
            return new Student
            {
                Id = id,
                Name = request.Name,
                Department = request.Department,
            };
        }

        // 3. Entity -> Response
        public static StudentResponse ToResponse(this Student student, string message = "Success")
        {
            return new StudentResponse
            {
                // BaseResponse Part
                Success = true,
                Message = message,
                Errors = null,

                // Student Part
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Department = student.Department,
                Cgpa = student.Cgpa,
            };
        }
    }
}
