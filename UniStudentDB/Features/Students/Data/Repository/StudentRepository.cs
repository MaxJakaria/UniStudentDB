using ErrorOr;
using Microsoft.EntityFrameworkCore;
using UniStudentDB.Features.Students.Data.Models;
using UniStudentDB.Features.Students.Domain.Entities;
using UniStudentDB.Features.Students.Domain.Repository;
using UniStudentDB.Infrastructure;

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
            catch (Exception ex)
            {
                return Error.Failure("Create.Failure", ex.Message);
            }
        }


        public async Task<ErrorOr<List<Student>>> GetAllStudentsAsync()
        {
            try
            {
                var models = await _context.Students.ToListAsync();
                // Model -> Entity কাস্টিং (যেহেতু প্যারেন্ট-চাইল্ড রিলেশন)
                return models.Cast<Student>().ToList();
            }
            catch (Exception ex)
            {
                return Error.Failure("Fetch.Failure", ex.Message);
            }
        }

        public async Task<ErrorOr<Updated>> UpdateStudentAsync(Student student)
        {
            try
            {
                var model = await _context.Students.FindAsync(student.Id);

                if(model is null)
                {
                    return Error.NotFound(description: "Student not found");
                }

                model.Name = student.Name;
                model.Email = student.Email;
                model.Department = student.Department;
                model.Cgpa = student.Cgpa;

                _context.Students.Update(model);
                await _context.SaveChangesAsync();
                return Result.Updated;

            }catch(Exception ex) {
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