using ErrorOr;
using UniStudentDB.Features.Students.Domain.Entities;
using UniStudentDB.Features.Students.Domain.Repository;

namespace UniStudentDB.Features.Students.Application.UseCases
{
    public class UpdateStudentUseCase
    {
        private readonly IStudentRepository _repository;

        public UpdateStudentUseCase(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<Updated>> ExecuteAsync(Student student)
        {
            return await _repository.UpdateStudentAsync(student);
        }
    }
}