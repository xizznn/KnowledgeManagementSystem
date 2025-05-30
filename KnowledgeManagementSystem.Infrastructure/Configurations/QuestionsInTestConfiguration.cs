using KnowledgeManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgeManagement.Infrastructure.Configurations
{
    public class QuestionsInTestConfiguration : IEntityTypeConfiguration<QuestionsInTest>
    {
        public void Configure(EntityTypeBuilder<QuestionsInTest> builder)
        {
            builder.HasKey(qt => new { qt.TestId, qt.QuestionId });

            builder.HasOne(qt => qt.Test)
                .WithMany(t => t.Questions)
                .HasForeignKey(qt => qt.TestId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(qt => qt.Question)
                .WithMany(q => q.Tests)
                .HasForeignKey(qt => qt.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}