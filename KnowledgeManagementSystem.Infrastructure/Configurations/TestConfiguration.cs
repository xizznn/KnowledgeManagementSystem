using KnowledgeManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgeManagement.Infrastructure.Configurations
{
    public class TestConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}