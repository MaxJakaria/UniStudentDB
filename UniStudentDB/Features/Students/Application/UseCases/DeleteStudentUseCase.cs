using ErrorOr;
using UniStudentDB.Features.Students.Domain.Repository;

namespace UniStudentDB.Features.Students.Application.UseCases
{
    public class DeleteStudentUseCase
    {
        private readonly IStudentRepository _repository;

        public DeleteStudentUseCase(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ErrorOr<Deleted>> ExecuteAsync(int id)
        {
            return await _repository.DeleteStudentAsync(id);
        }
    }
}