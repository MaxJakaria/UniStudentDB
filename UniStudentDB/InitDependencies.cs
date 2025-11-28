using FluentValidation;
using Microsoft.EntityFrameworkCore;
using UniStudentDB.Features.Students.Application.UseCases;
using UniStudentDB.Features.Students.Application.Validators;
using UniStudentDB.Features.Students.Data.Repository;
using UniStudentDB.Features.Students.Domain.Entities;
using UniStudentDB.Features.Students.Domain.Repository;

namespace UniStudentDB
{
    // Extension Method (class & method -> 'static', 'this' keyword befor parameter) 
    public static class InitDependencies
    {
        public static IServiceCollection AddAppDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // 1. Database Configuration
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // 2. Repository Injection (Data Layer)
            services.AddScoped<IStudentRepository, StudentRepository>();

            // Validator Injection
            services.AddScoped<IValidator<Student>, CreateStudentValidator>();

            // 3. UseCase Injection (Application Layer)
            services.AddScoped<CreateStudentUseCase>();
            services.AddScoped<GetAllStudentsUseCase>();
            services.AddScoped<UpdateStudentUseCase>();
            services.AddScoped<DeleteStudentUseCase>();

            return services;
        }
    }
}