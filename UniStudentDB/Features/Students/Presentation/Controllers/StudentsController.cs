using Microsoft.AspNetCore.Mvc;
using UniStudentDB.Features.Students.Application.UseCases;
using UniStudentDB.Features.Students.Domain.Entities;

namespace UniStudentDB.Features.Students.Presentation
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly CreateStudentUseCase _createUseCase;
        private readonly GetAllStudentsUseCase _getAllUseCase;

        // Constructor Injection
        public StudentsController(CreateStudentUseCase createUseCase, GetAllStudentsUseCase getAllUseCase)
        {
            _createUseCase = createUseCase;
            _getAllUseCase = getAllUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            var result = await _createUseCase.ExecuteAsync(student);

            // ErrorOr এর ম্যাজিক: এরর থাকলে এরর দিবে, না হলে সাকসেস
            return result.Match(
                created => Ok("Student Created Successfully"),
                errors => Problem(errors[0].Description)
            );
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _getAllUseCase.ExecuteAsync();

            return result.Match(
                students => Ok(students),
                errors => Problem(errors[0].Description)
            );
        }
    }
}