using Microsoft.EntityFrameworkCore;
using UniStudentDB;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// 1. Service Registration (Dependency Injection)
// ==========================================

// Controller Setup
builder.Services.AddControllers();

// Swagger/OpenAPI Setup (Testing UI ?? ????)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Custom DI
builder.Services.AddAppDependencies(builder.Configuration);

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

// Presentation Layer
app.MapControllers();

app.Run();