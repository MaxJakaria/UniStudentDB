using ErrorOr;
using UniStudentDB.Core.Models;
using UniStudentDB.Features.Students.Domain.Entities;

namespace UniStudentDB.Features.Students.Domain.Repository
{
    public interface IStudentRepository
    {
        Task<ErrorOr<Created>> AddStudentAsync(Student student);
        Task<ErrorOr<PagedResponse<Student>>> GetAllStudentsAsync(
            string? searchTerm,
            string? department,
            int pageNumber,
            int pageSize
        );
        Task<ErrorOr<Student>> GetStudentByIdAsync(int id);

        Task<ErrorOr<Updated>> UpdateStudentAsync(Student student);
        Task<ErrorOr<Deleted>> DeleteStudentAsync(int id);
    }
}
