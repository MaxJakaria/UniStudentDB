using UniStudentDB.Core.Models;

namespace UniStudentDB.Features.Students.Presentation.Dto
{
    public class StudentResponse : BaseResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public double Cgpa { get; set; }
    }
}