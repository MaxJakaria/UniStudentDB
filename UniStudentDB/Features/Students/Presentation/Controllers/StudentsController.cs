using ErrorOr;
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
        private readonly UpdateStudentUseCase _updateUseCase;
        private readonly DeleteStudentUseCase _deleteUseCase;

        // Constructor Injection
        public StudentsController(
            CreateStudentUseCase createUseCase,
            GetAllStudentsUseCase getAllUseCase,
            UpdateStudentUseCase updateUseCase,
            DeleteStudentUseCase deleteUseCase)
        {
            _createUseCase = createUseCase;
            _getAllUseCase = getAllUseCase;
            _updateUseCase = updateUseCase;
            _deleteUseCase = deleteUseCase;
        }

        // --- POST: Create ---
        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            var result = await _createUseCase.ExecuteAsync(student);

            // ErrorOr : If error occure then give error otherwise not
            return result.Match(
                created => Ok("Student Created Successfully"),
                errors => {
                    var firstError = errors[0];
                    if (firstError.Type == ErrorType.Conflict)
                    {
                        return Problem(detail: firstError.Description, statusCode: 409); // 409 Conflict
                    }

                    return Problem(detail: firstError.Description, statusCode: firstError.Type == ErrorType.Validation ? 400 : 500);
                }
            );
        }

        // --- GET: Read All ---
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _getAllUseCase.ExecuteAsync();

            return result.Match(
                students => Ok(students),
                errors => Problem(errors[0].Description)
            );
        }

        // --- PUT: Update (New) ---
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Student student)
        {
            // Security check by Id
            if (id != student.Id) return BadRequest("ID mismatch");

            var result = await _updateUseCase.ExecuteAsync(student);

            return result.Match(
                updated => Ok(new { message = "Updated Successfully" }),
                errors => Problem(errors[0].Description)
            );
        }

        // --- DELETE: Delete (New) ---
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _deleteUseCase.ExecuteAsync(id);

            return result.Match(
                deleted => Ok(new { message = "Deleted Successfully" }),
                errors => Problem(errors[0].Description)
            );
        }
    }
}