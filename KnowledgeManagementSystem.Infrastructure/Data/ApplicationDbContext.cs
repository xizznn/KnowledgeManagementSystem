using KnowledgeManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace KnowledgeManagementSystem.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CourseEntity> Courses { get; set; } = null!;
        public DbSet<TestEntity> Tests { get; set; } = null!;
        public DbSet<UserEntity> Users { get; set; } = null!;
        public DbSet<RoleEntity> Roles { get; set; } = null!;
        public DbSet<QuestionEntity> Questions { get; set; } = null!;
        public DbSet<QuestionInTestEntity> QuestionsInTests { get; set; } = null!;
        public DbSet<TestInCourseEntity> TestsInCourses { get; set; } = null!;
        public DbSet<UserOnCourseEntity> UsersOnCourses { get; set; } = null!;
        public DbSet<FavoriteCourseEntity> FavoriteCourses { get; set; } = null!;
        public DbSet<TestResultEntity> TestResults { get; set; } = null!;
        public DbSet<FavoriteTestEntity> FavoriteTests { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // конфигурация UserEntity
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.PasswordHash).HasColumnName("password_hash");
                entity.Property(u => u.PasswordSalt).HasColumnName("password_salt");
                entity.Property(u => u.RefreshToken).HasColumnName("refresh_token");
                entity.Property(u => u.RefreshTokenExpiry).HasColumnName("refresh_token_expiry");

                entity.HasOne(u => u.Role)
                    .WithMany(r => r.Users)
                    .HasForeignKey(u => u.RoleId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // конфигурация RoleEntity
            modelBuilder.Entity<RoleEntity>(entity =>
            {
                entity.HasData(
                    new RoleEntity { Id = 1, Title = "Admin" },
                    new RoleEntity { Id = 2, Title = "User" }
                );
            });

            // конфигурация CourseEntity
            modelBuilder.Entity<CourseEntity>(entity =>
            {
                entity.HasIndex(c => c.Title).IsUnique();
                entity.Property(c => c.UserAuthor).IsRequired();

                entity.HasMany(c => c.Tests)
                    .WithOne(t => t.Course)
                    .HasForeignKey(t => t.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(c => c.Users)
                    .WithOne(u => u.Course)
                    .HasForeignKey(u => u.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // конфигурация TestEntity
            modelBuilder.Entity<TestEntity>(entity =>
            {
                entity.HasIndex(t => t.Title).IsUnique();
            });
            // конфигурация TestResultEntity
            modelBuilder.Entity<TestResultEntity>(entity =>
            {
                entity.HasKey(tr => tr.Id);

                entity.HasOne(tr => tr.User)
                    .WithMany()
                    .HasForeignKey(tr => tr.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(tr => tr.Test)
                    .WithMany()
                    .HasForeignKey(tr => tr.TestId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Property(tr => tr.DateTaken)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<FavoriteTestEntity>()
            .HasKey(f => new { f.UserId, f.TestId });

            modelBuilder.Entity<FavoriteTestEntity>(entity =>
            {
                entity.Property(f => f.AddedDate)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP")
                      .ValueGeneratedOnAdd();

                entity.HasOne(f => f.User)
                      .WithMany()
                      .HasForeignKey(f => f.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(f => f.Test)
                      .WithMany()
                      .HasForeignKey(f => f.TestId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // составные ключи
            modelBuilder.Entity<QuestionInTestEntity>()
                .HasKey(qt => new { qt.TestId, qt.QuestionId });

            modelBuilder.Entity<TestInCourseEntity>()
                .HasKey(tc => new { tc.CourseId, tc.TestId });

            modelBuilder.Entity<UserOnCourseEntity>()
                .HasKey(uc => new { uc.UserId, uc.CourseId });

            modelBuilder.Entity<FavoriteCourseEntity>()
                .HasKey(f => new { f.UserId, f.CourseId });

            // отношения для QuestionInTest
            modelBuilder.Entity<QuestionInTestEntity>()
                .HasOne(qt => qt.Question)
                .WithMany()
                .HasForeignKey(qt => qt.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<QuestionInTestEntity>()
                .HasOne(qt => qt.Test)
                .WithMany()
                .HasForeignKey(qt => qt.TestId)
                .OnDelete(DeleteBehavior.Cascade);

            // отношения для FavoriteCourses
            modelBuilder.Entity<FavoriteCourseEntity>(entity =>
            {
                entity.Property(f => f.AddedDate)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAdd();

                entity.HasOne(f => f.User)
                    .WithMany()
                    .HasForeignKey(f => f.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(f => f.Course)
                    .WithMany()
                    .HasForeignKey(f => f.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);
            }); 

            // применение конфигураций из сборки
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.ClrType.FullName?.Contains("Microsoft.EntityFrameworkCore") == true)
                    continue;

                entityType.SetTableName(ToSnakeCase(entityType.GetTableName()));

                foreach (var property in entityType.GetProperties())
                {
                    property.SetColumnName(ToSnakeCase(property.GetColumnName()));
                }

                foreach (var key in entityType.GetKeys())
                {
                    key.SetName(ToSnakeCase(key.GetName()));
                }

                foreach (var index in entityType.GetIndexes())
                {
                    index.SetDatabaseName(ToSnakeCase(index.GetDatabaseName()));
                }

                foreach (var foreignKey in entityType.GetForeignKeys())
                {
                    foreignKey.SetConstraintName(ToSnakeCase(foreignKey.GetConstraintName()));
                }
            }
        }

        [return: NotNullIfNotNull(nameof(input))]
        private static string? ToSnakeCase(string? input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var buffer = new StringBuilder(input.Length * 2);
            buffer.Append(char.ToLowerInvariant(input[0]));

            for (int i = 1; i < input.Length; i++)
            {
                if (char.IsUpper(input[i]))
                {
                    buffer.Append('_');
                    buffer.Append(char.ToLowerInvariant(input[i]));
                }
                else
                {
                    buffer.Append(input[i]);
                }
            }

            return buffer.ToString();
        }
    }
}