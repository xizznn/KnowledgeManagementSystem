using KnowledgeManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgeManagement.Infrastructure.Configurations
{
    public class TestResultConfiguration : IEntityTypeConfiguration<TestResultEntity>
    {
        public void Configure(EntityTypeBuilder<TestResultEntity> builder)
        {
            builder.HasKey(tr => tr.Id);

            builder.Property(tr => tr.Score)
                .IsRequired(false);

            builder.Property(tr => tr.DateTaken)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasOne(tr => tr.User)
                .WithMany()
                .HasForeignKey(tr => tr.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(tr => tr.Test)
                .WithMany()
                .HasForeignKey(tr => tr.TestId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
