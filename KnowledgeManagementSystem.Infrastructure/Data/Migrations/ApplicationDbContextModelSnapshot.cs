﻿// <auto-generated />
using System;
using KnowledgeManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KnowledgeManagementSystem.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("KnowledgeManagementSystem.Core.Entities.CourseEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("added_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<DateTime?>("EditedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("edited_at");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("title");

                    b.Property<string>("UserAuthor")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("user_author");

                    b.HasKey("Id")
                        .HasName("p_k_courses");

                    b.HasIndex("Title")
                        .IsUnique()
                        .HasDatabaseName("i_x_courses_title");

                    b.ToTable("courses");
                });

            modelBuilder.Entity("KnowledgeManagementSystem.Core.Entities.FavoriteCourseEntity", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<int>("CourseId")
                        .HasColumnType("integer")
                        .HasColumnName("course_id");

                    b.Property<DateTime>("AddedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("added_date")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("UserId", "CourseId")
                        .HasName("p_k_favorite_courses");

                    b.HasIndex("CourseId")
                        .HasDatabaseName("i_x_favorite_courses_course_id");

                    b.ToTable("favorite_courses");
                });

            modelBuilder.Entity("KnowledgeManagementSystem.Core.Entities.FavoriteTestEntity", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<int>("TestId")
                        .HasColumnType("integer")
                        .HasColumnName("test_id");

                    b.Property<DateTime>("AddedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("added_date")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("UserId", "TestId")
                        .HasName("p_k_favorite_tests");

                    b.HasIndex("TestId")
                        .HasDatabaseName("i_x_favorite_tests_test_id");

                    b.ToTable("favorite_tests");
                });

            modelBuilder.Entity("KnowledgeManagementSystem.Core.Entities.QuestionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("answer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("p_k_questions");

                    b.ToTable("questions");
                });

            modelBuilder.Entity("KnowledgeManagementSystem.Core.Entities.QuestionInTestEntity", b =>
                {
                    b.Property<int>("TestId")
                        .HasColumnType("integer")
                        .HasColumnName("test_id");

                    b.Property<int>("QuestionId")
                        .HasColumnType("integer")
                        .HasColumnName("question_id");

                    b.HasKey("TestId", "QuestionId")
                        .HasName("p_k_questions_in_tests");

                    b.HasIndex("QuestionId")
                        .HasDatabaseName("i_x_questions_in_tests_question_id");

                    b.ToTable("questions_in_tests");
                });

            modelBuilder.Entity("KnowledgeManagementSystem.Core.Entities.RoleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("p_k_roles");

                    b.ToTable("roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Title = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Title = "User"
                        });
                });

            modelBuilder.Entity("KnowledgeManagementSystem.Core.Entities.TestEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("added_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int?>("CourseId")
                        .HasColumnType("integer")
                        .HasColumnName("course_id");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<DateTime?>("EditedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("edited_at");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("p_k_tests");

                    b.HasIndex("CourseId")
                        .HasDatabaseName("i_x_tests_course_id");

                    b.HasIndex("Title")
                        .IsUnique()
                        .HasDatabaseName("i_x_tests_title");

                    b.ToTable("tests");
                });

            modelBuilder.Entity("KnowledgeManagementSystem.Core.Entities.TestInCourseEntity", b =>
                {
                    b.Property<int>("CourseId")
                        .HasColumnType("integer")
                        .HasColumnName("course_id");

                    b.Property<int>("TestId")
                        .HasColumnType("integer")
                        .HasColumnName("test_id");

                    b.HasKey("CourseId", "TestId")
                        .HasName("p_k_tests_in_courses");

                    b.HasIndex("TestId")
                        .HasDatabaseName("i_x_tests_in_courses_test_id");

                    b.ToTable("tests_in_courses");
                });

            modelBuilder.Entity("KnowledgeManagementSystem.Core.Entities.TestResultEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateTaken")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_taken")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int?>("Score")
                        .HasColumnType("integer")
                        .HasColumnName("score");

                    b.Property<int>("TestId")
                        .HasColumnType("integer")
                        .HasColumnName("test_id");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("p_k_test_results");

                    b.HasIndex("TestId")
                        .HasDatabaseName("i_x_test_results_test_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("i_x_test_results_user_id");

                    b.ToTable("test_results");
                });

            modelBuilder.Entity("KnowledgeManagementSystem.Core.Entities.UserOnCourseEntity", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<int>("CourseId")
                        .HasColumnType("integer")
                        .HasColumnName("course_id");

                    b.HasKey("UserId", "CourseId")
                        .HasName("p_k_users_on_courses");

                    b.HasIndex("CourseId")
                        .HasDatabaseName("i_x_users_on_courses_course_id");

                    b.ToTable("users_on_courses");
                });

            modelBuilder.Entity("UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("password_hash");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("password_salt");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text")
                        .HasColumnName("refresh_token");

                    b.Property<DateTime?>("RefreshTokenExpiry")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("refresh_token_expiry");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("surname");

                    b.HasKey("Id")
                        .HasName("p_k_users");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("i_x_users_email");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("i_x_users_role_id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("KnowledgeManagementSystem.Core.Entities.FavoriteCourseEntity", b =>
                {
                    b.HasOne("KnowledgeManagementSystem.Core.Entities.CourseEntity", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_favorite_courses_courses_course_id");

                    b.HasOne("UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_favorite_courses__users_user_id");

                    b.Navigation("Course");

                    b.Navigation("User");
                });

            modelBuilder.Entity("KnowledgeManagementSystem.Core.Entities.FavoriteTestEntity", b =>
                {
                    b.HasOne("KnowledgeManagementSystem.Core.Entities.TestEntity", "Test")
                        .WithMany()
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_favorite_tests__tests_test_id");

                    b.HasOne("UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_favorite_tests__users_user_id");

                    b.Navigation("Test");

                    b.Navigation("User");
                });

            modelBuilder.Entity("KnowledgeManagementSystem.Core.Entities.QuestionInTestEntity", b =>
                {
                    b.HasOne("KnowledgeManagementSystem.Core.Entities.QuestionEntity", "Question")
                        .WithMany("Tests")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_questions_in_tests_questions_question_id");

                    b.HasOne("KnowledgeManagementSystem.Core.Entities.TestEntity", "Test")
                        .WithMany("Questions")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_questions_in_tests__tests_test_id");

                    b.Navigation("Question");

                    b.Navigation("Test");
                });

            modelBuilder.Entity("KnowledgeManagementSystem.Core.Entities.TestEntity", b =>
                {
                    b.HasOne("KnowledgeManagementSystem.Core.Entities.CourseEntity", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .HasConstraintName("f_k_tests_courses_course_id");

                    b.Navigation("Course");
                });

            modelBuilder.Entity("KnowledgeManagementSystem.Core.Entities.TestInCourseEntity", b =>
                {
                    b.HasOne("KnowledgeManagementSystem.Core.Entities.CourseEntity", "Course")
                        .WithMany("Tests")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_tests_in_courses_courses_course_id");

                    b.HasOne("KnowledgeManagementSystem.Core.Entities.TestEntity", "Test")
                        .WithMany("Courses")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_tests_in_courses_tests_test_id");

                    b.Navigation("Course");

                    b.Navigation("Test");
                });

            modelBuilder.Entity("KnowledgeManagementSystem.Core.Entities.TestResultEntity", b =>
                {
                    b.HasOne("KnowledgeManagementSystem.Core.Entities.TestEntity", "Test")
                        .WithMany()
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_test_results_tests_test_id");

                    b.HasOne("UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_test_results__users_user_id");

                    b.Navigation("Test");

                    b.Navigation("User");
                });

            modelBuilder.Entity("KnowledgeManagementSystem.Core.Entities.UserOnCourseEntity", b =>
                {
                    b.HasOne("KnowledgeManagementSystem.Core.Entities.CourseEntity", "Course")
                        .WithMany("Users")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_users_on_courses_courses_course_id");

                    b.HasOne("UserEntity", "User")
                        .WithMany("Courses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_users_on_courses__users_user_id");

                    b.Navigation("Course");

                    b.Navigation("User");
                });

            modelBuilder.Entity("UserEntity", b =>
                {
                    b.HasOne("KnowledgeManagementSystem.Core.Entities.RoleEntity", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("f_k_users_roles_role_id");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("KnowledgeManagementSystem.Core.Entities.CourseEntity", b =>
                {
                    b.Navigation("Tests");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("KnowledgeManagementSystem.Core.Entities.QuestionEntity", b =>
                {
                    b.Navigation("Tests");
                });

            modelBuilder.Entity("KnowledgeManagementSystem.Core.Entities.RoleEntity", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("KnowledgeManagementSystem.Core.Entities.TestEntity", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("Questions");
                });

            modelBuilder.Entity("UserEntity", b =>
                {
                    b.Navigation("Courses");
                });
#pragma warning restore 612, 618
        }
    }
}
