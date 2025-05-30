using KnowledgeManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgeManagement.Infrastructure.Configurations
{
    public class UserOnCourseConfiguration : IEntityTypeConfiguration<UserOnCourse>
    {
        public void Configure(EntityTypeBuilder<UserOnCourse> builder)
        {
            builder.HasKey(uc => new { uc.UserId, uc.CourseId });

            builder.HasOne(uc => uc.User)
                .WithMany(u => u.Courses)
                .HasForeignKey(uc => uc.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(uc => uc.Course)
                .WithMany(c => c.Users)
                .HasForeignKey(uc => uc.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}