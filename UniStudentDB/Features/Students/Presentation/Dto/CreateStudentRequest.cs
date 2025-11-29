namespace UniStudentDB.Features.Students.Presentation.Dto
{
    public record CreateStudentRequest(
        string Name,
        string Email,
        string Department,
        double Cgpa
    );
}