using KnowledgeManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgeManagement.Infrastructure.Configurations
{
    public class TestInCourseConfiguration : IEntityTypeConfiguration<TestInCourse>
    {
        public void Configure(EntityTypeBuilder<TestInCourse> builder)
        {
            builder.HasKey(tc => new { tc.CourseId, tc.TestId });

            builder.HasOne(tc => tc.Course)
                .WithMany(c => c.Tests)
                .HasForeignKey(tc => tc.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(tc => tc.Test)
                .WithMany(t => t.Courses)
                .HasForeignKey(tc => tc.TestId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}