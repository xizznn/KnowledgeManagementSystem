using KnowledgeManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class FavoriteCourseConfiguration : IEntityTypeConfiguration<FavoriteCourseEntity>
{
    public void Configure(EntityTypeBuilder<FavoriteCourseEntity> builder)
    {
        builder.HasKey(f => new { f.UserId, f.CourseId });

        builder.HasOne(f => f.User)
            .WithMany()
            .HasForeignKey(f => f.UserId);

        builder.HasOne(f => f.Course)
            .WithMany()
            .HasForeignKey(f => f.CourseId);
    }
}