using ErrorOr;
using UniStudentDB.Core.Models;
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

        public async Task<ErrorOr<PagedResponse<Student>>> ExecuteAsync(
            string? searchTerm,
            string? department,
            int pageNumber,
            int pageSize
        )
        {
            return await _repository.GetAllStudentsAsync(
                searchTerm,
                department,
                pageNumber,
                pageSize
            );
        }
    }
}
