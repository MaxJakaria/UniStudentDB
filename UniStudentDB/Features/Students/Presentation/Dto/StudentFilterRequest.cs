namespace UniStudentDB.Features.Students.Presentation.Dto
{
    public record StudentFilterRequest(
        string? SearchTerm,
        string? Department,
        int PageNumber = 1, // Default page 1
        int PageSize = 10 // Default 10 items per page
    );
}
