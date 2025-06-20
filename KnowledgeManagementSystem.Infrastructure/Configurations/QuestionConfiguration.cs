using KnowledgeManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgeManagement.Infrastructure.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<QuestionEntity>
    {
        public void Configure(EntityTypeBuilder<QuestionEntity> builder)
        {
            builder.HasKey(q => q.Id);

            builder.Property(q => q.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(q => q.Answer)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}