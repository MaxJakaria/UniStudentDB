using FluentValidation;
using UniStudentDB.Features.Students.Domain.Entities;

namespace UniStudentDB.Features.Students.Application.Validators
{
    public class CreateStudentValidator : AbstractValidator<Student>
    {
        public CreateStudentValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name cannot be empty")
                .MaximumLength(50).WithMessage("Name must be less than 50 characters");

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress().WithMessage("A valid email is required");

            RuleFor(x => x.Department)
                .NotEmpty();

            RuleFor(x => x.Cgpa)
                .InclusiveBetween(0.0, 4.0).WithMessage("CGPA must be between 0.0 and 4.0");
        }
    }
}