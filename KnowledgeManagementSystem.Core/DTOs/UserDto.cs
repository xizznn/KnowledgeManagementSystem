using System.ComponentModel.DataAnnotations;

namespace KnowledgeManagementSystem.Core.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public int RoleId { get; set; }
        public string RoleTitle { get; set; } = null!;

    }

    public class RegisterUserDto
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public int RoleId { get; set; } = 2; // по умолчанию роль user
    }


    public class UpdateUserDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Password { get; set; }
        public int RoleId { get; set; }
    }



    public class UserProfileDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string RoleTitle { get; set; } = null!;
        public IEnumerable<CourseDto> EnrolledCourses { get; set; } = new List<CourseDto>();
    }
}