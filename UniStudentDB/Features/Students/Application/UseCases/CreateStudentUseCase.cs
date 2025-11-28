using ErrorOr;
using FluentValidation;
using UniStudentDB.Features.Students.Domain.Entities;
using UniStudentDB.Features.Students.Domain.Repository;

namespace UniStudentDB.Features.Students.Application.UseCases
{
    public class CreateStudentUseCase
    {
        private readonly IStudentRepository _repository;
        private readonly IValidator<Student> _validator;

        public CreateStudentUseCase(IStudentRepository repository, IValidator<Student> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<ErrorOr<Created>> ExecuteAsync(Student student)
        {
            // Check Validation and Uniqueness
            var validationResult = await _validator.ValidateAsync(student);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(validationFailure => Error.Validation(
                        code: validationFailure.PropertyName,
                        description: validationFailure.ErrorMessage))
                    .ToList();

                return errors;
            }

            // Save to database
            return await _repository.AddStudentAsync(student);
        }
    }
}