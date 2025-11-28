using Microsoft.EntityFrameworkCore;
using UniStudentDB.Features.Students.Data.Models;

namespace UniStudentDB.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // ডাটাবেস টেবিল Model চেনে, Entity না
        public DbSet<StudentModel> Students { get; set; }
    }
}