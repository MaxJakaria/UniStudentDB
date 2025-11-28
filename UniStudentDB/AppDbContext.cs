using Microsoft.EntityFrameworkCore;
using UniStudentDB.Features.Students.Data.Models;

namespace UniStudentDB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }      
       
        public DbSet<StudentModel> Students { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Automatically apply all configurations from the assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}