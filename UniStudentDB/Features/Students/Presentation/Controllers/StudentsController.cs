using Microsoft.AspNetCore.Mvc;
using UniStudentDB.Core.Models;
using UniStudentDB.Features.Students.Application.UseCases;
using UniStudentDB.Features.Students.Presentation.Dto;

namespace UniStudentDB.Features.Students.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly CreateStudentUseCase _createUseCase;
        private readonly GetAllStudentsUseCase _getAllUseCase;
        private readonly GetStudentByIdUseCase _getByIdUseCase;
        private readonly UpdateStudentUseCase _updateUseCase;
        private readonly DeleteStudentUseCase _deleteUseCase;

        public StudentsController(
            CreateStudentUseCase createUseCase,
            GetAllStudentsUseCase getAllUseCase,
            GetStudentByIdUseCase getByIdUseCase,
            UpdateStudentUseCase updateUseCase,
            DeleteStudentUseCase deleteUseCase
        )
        {
            _createUseCase = createUseCase;
            _getAllUseCase = getAllUseCase;
            _getByIdUseCase = getByIdUseCase;
            _updateUseCase = updateUseCase;
            _deleteUseCase = deleteUseCase;
        }

        // --- POST (Create) ---
        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentRequest request)
        {
            var entity = request.ToEntity();
            var result = await _createUseCase.ExecuteAsync(entity);

            return result.Match(
                // Entity -> StudentResponse
                created =>
                    Ok(
                        new StudentResponse
                        {
                            Success = true,
                            Message = "Student Created Successfully",
                        }
                    ),
                errors => Problem(errors[0].Description)
            );
        }

        // --- GET (Read All) ---
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _getAllUseCase.ExecuteAsync();

            return result.Match(
                // List<Student> -> List<StudentResponse>
                students => Ok(students.Select(s => s.ToResponse("Data fetched"))),
                errors => Problem(errors[0].Description)
            );
        }

        // --- GET Single Student ---
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _getByIdUseCase.ExecuteAsync(id);

            return result.Match(
                student => Ok(student.ToResponse("Student found")),
                errors => Problem(errors[0].Description)
            );
        }

        // --- PUT (Update) ---
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateStudentRequest request)
        {
            var entity = request.ToEntity(id);
            var result = await _updateUseCase.ExecuteAsync(entity);

            return result.Match(
                // return BaseResponse
                updated =>
                    Ok(new BaseResponse { Success = true, Message = "Updated Successfully" }),
                errors => Problem(errors[0].Description)
            );
        }

        // --- DELETE (Delete) ---
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _deleteUseCase.ExecuteAsync(id);

            return result.Match(
                deleted =>
                    Ok(new BaseResponse { Success = true, Message = "Deleted Successfully" }),
                errors => Problem(errors[0].Description)
            );
        }
    }
}
