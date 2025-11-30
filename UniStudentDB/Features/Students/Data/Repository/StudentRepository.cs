using ErrorOr;
using Microsoft.EntityFrameworkCore;
using UniStudentDB.Features.Students.Data.Models;
using UniStudentDB.Features.Students.Domain.Entities;
using UniStudentDB.Features.Students.Domain.Repository;

namespace UniStudentDB.Features.Students.Data.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ErrorOr<Created>> AddStudentAsync(Student student)
        {
            try
            {
                var model = StudentModel.FromEntity(student);
                await _context.Students.AddAsync(model);
                await _context.SaveChangesAsync();
                return Result.Created;
            }
            catch (DbUpdateException ex)
            {
                // duplicte error code 2601/ 2627
                if (
                    ex.InnerException != null
                    && (
                        ex.InnerException.Message.ToLower().Contains("duplicate")
                        || ex.InnerException.Message.ToLower().Contains("unique")
                    )
                )
                {
                    return Error.Conflict("Student.DuplicateEmail", "Email already exists.");
                }
                return Error.Failure("Database.Error", ex.Message);
            }
            catch (Exception ex)
            {
                return Error.Failure("Create.Failure", ex.Message);
            }
        }

        public async Task<ErrorOr<List<Student>>> GetAllStudentsAsync(
            string? searchTerm,
            string? department,
            int pageNumber,
            int pageSize
        )
        {
            try
            {
                // 1. Queryable
                var query = _context.Students.AsQueryable();

                // Searching: if searchTerm is provided
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    // SQL: WHERE Name LIKE '%searchTerm%'
                    query = query.Where(s => s.Name.Contains(searchTerm.ToLower()));
                }

                // Filtering: if department is provided
                if (!string.IsNullOrWhiteSpace(department))
                {
                    // SQL: AND Department = 'department'
                    query = query.Where(s => s.Department == department.ToLower());
                }

                // 2. Pagination Logic
                // Formula: Skip = (Page - 1) * Size
                var skipCount = (pageNumber - 1) * pageSize;

                // 3. Execution
                var models = await query
                    .Skip(skipCount) // Skip previous pages
                    .Take(pageSize) // Take only current page items
                    .ToListAsync();

                // Model -> Entity [casting]
                return models.Cast<Student>().ToList();
            }
            catch (Exception ex)
            {
                return Error.Failure("Fetch.Failure", ex.Message);
            }
        }

        public async Task<ErrorOr<Student>> GetStudentByIdAsync(int id)
        {
            try
            {
                var model = await _context.Students.FindAsync(id);

                if (model is null)
                {
                    return Error.NotFound(description: "Student not found");
                }

                // Model -> Entity [auto-cast]
                return model;
            }
            catch (Exception ex)
            {
                return Error.Failure("GetStudentById.Failure", ex.Message);
            }
        }

        public async Task<ErrorOr<Updated>> UpdateStudentAsync(Student student)
        {
            try
            {
                var model = await _context.Students.FindAsync(student.Id);

                if (model is null)
                {
                    return Error.NotFound(description: "Student not found");
                }

                if (!string.IsNullOrWhiteSpace(student.Name))
                {
                    model.Name = student.Name;
                }
                if (!string.IsNullOrWhiteSpace(student.Email))
                {
                    model.Email = student.Email;
                }

                _context.Students.Update(model);
                await _context.SaveChangesAsync();
                return Result.Updated;
            }
            catch (Exception ex)
            {
                return Error.Failure("UpdateStudent.Failure", ex.Message);
            }
        }

        public async Task<ErrorOr<Deleted>> DeleteStudentAsync(int id)
        {
            try
            {
                var model = await _context.Students.FindAsync(id);

                if (model is null)
                {
                    return Error.NotFound(description: "Student not found");
                }

                _context.Students.Remove(model);
                await _context.SaveChangesAsync();

                return Result.Deleted;
            }
            catch (Exception ex)
            {
                return Error.Failure("DeleteStudent.Failure", ex.Message);
            }
        }
    }
}
