using ErrorOr;
using UniStudentDB.Features.Students.Domain.Entities;

namespace UniStudentDB.Features.Students.Domain.Repository
{
    public interface IStudentRepository
    {
        Task<ErrorOr<List<Student>>> GetAllStudentsAsync();
        Task<ErrorOr<Created>> AddStudentAsync(Student student);

        Task<ErrorOr<Updated>> UpdateStudentAsync(Student student);
        Task<ErrorOr<Deleted>> DeleteStudentAsync(int id);
    }
}
