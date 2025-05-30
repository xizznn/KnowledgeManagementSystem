using KnowledgeManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Emit;
using static System.Net.Mime.MediaTypeNames;

namespace KnowledgeManagementSystem.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<QuestionsInTest> QuestionsInTests { get; set; }
        public DbSet<TestInCourse> TestsInCourses { get; set; }
        public DbSet<UserOnCourse> UsersOnCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Конфигурация связей и ключей
            modelBuilder.Entity<QuestionsInTest>()
                .HasKey(qt => new { qt.TestId, qt.QuestionId });

            modelBuilder.Entity<TestInCourse>()
                .HasKey(tc => new { tc.CourseId, tc.TestId });

            modelBuilder.Entity<UserOnCourse>()
                .HasKey(uc => new { uc.UserId, uc.CourseId });

            // Дополнительные настройки моделей
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}