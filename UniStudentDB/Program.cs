using Microsoft.EntityFrameworkCore;
using UniStudentDB.Features.Students.Application.UseCases;
using UniStudentDB.Features.Students.Data.Repository;
using UniStudentDB.Features.Students.Domain.Repository;
using UniStudentDB.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// 1. Service Registration (Dependency Injection)
// ==========================================

// A. Controller Setup
builder.Services.AddControllers();

// B. Swagger/OpenAPI Setup (Testing UI ?? ????)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// C. Database Setup (Infrastructure Layer)
// appsettings.json ???? 'DefaultConnection' ???? SQL Server ??????? ??? ?????
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// D. Clean Architecture Dependencies (Flutter ?? GetIt ?? ???)

// 1. Repository Injection:
// ???? ??? 'IStudentRepository' ?????, ???? 'StudentRepository' (Data Layer) ???? ?????
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

// 2. UseCase Injection:
// Controller ??? UseCase ?????, ??? ?? ????????? ???????? ?????
builder.Services.AddScoped<CreateStudentUseCase>();
builder.Services.AddScoped<GetAllStudentsUseCase>();


// ==========================================
// 2. Build App & Middleware Pipeline
// ==========================================
var app = builder.Build();

// Development ???? ????? Swagger ???? ????
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// ?? ?????????? (Presentation Layer) ????? ??? ?????
app.MapControllers();

// ????? ??? ???
app.Run();