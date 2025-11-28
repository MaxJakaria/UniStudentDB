using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniStudentDB.Features.Students.Data.Models;

namespace UniStudentDB.Features.Students.Data.Configuration
{
    public class StudentConfiguration : IEntityTypeConfiguration<StudentModel>
    {
        public void Configure(EntityTypeBuilder<StudentModel> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(100)
                .IsRequired();

            // Unique Email Constraint
            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.Department)
                .HasMaxLength(50);
        }
    }
}