using ErrorOr;
using UniStudentDB.Features.Students.Domain.Entities;
using UniStudentDB.Features.Students.Domain.Repository;

namespace UniStudentDB.Features.Students.Application.UseCases
{
    public class GetStudentByIdUseCase
    {
        private readonly IStudentRepository _repository;

        public GetStudentByIdUseCase(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<Student>> ExecuteAsync(int id)
        {
            return await _repository.GetStudentByIdAsync(id);
        }
    }
}