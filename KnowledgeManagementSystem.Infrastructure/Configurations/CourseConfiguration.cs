using KnowledgeManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgeManagement.Infrastructure.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.UserAuthor)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}