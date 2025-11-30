using ErrorOr;
using UniStudentDB.Features.Students.Domain.Entities;
using UniStudentDB.Features.Students.Domain.Repository;

namespace UniStudentDB.Features.Students.Application.UseCases
{
    public class GetAllStudentsUseCase
    {
        private readonly IStudentRepository _repository;

        public GetAllStudentsUseCase(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<List<Student>>> ExecuteAsync(
            string? searchTerm,
            string? department
        )
        {
            return await _repository.GetAllStudentsAsync(searchTerm, department);
        }
    }
}
