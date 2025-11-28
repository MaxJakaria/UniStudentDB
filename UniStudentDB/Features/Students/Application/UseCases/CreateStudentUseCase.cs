using ErrorOr;
using UniStudentDB.Features.Students.Domain.Entities;
using UniStudentDB.Features.Students.Domain.Repository;

namespace UniStudentDB.Features.Students.Application.UseCases
{
    public class CreateStudentUseCase
    {
        private readonly IStudentRepository _repository;

        public CreateStudentUseCase(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<Created>> ExecuteAsync(Student student)
        {
            return await _repository.AddStudentAsync(student);
        }
    }
}