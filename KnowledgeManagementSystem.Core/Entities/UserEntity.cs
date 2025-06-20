using KnowledgeManagementSystem.Core.Entities;

public class UserEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public byte[] PasswordHash { get; set; } = null!;
    public byte[] PasswordSalt { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public int RoleId { get; set; }
    public RoleEntity Role { get; set; } = null!;
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
    public ICollection<UserOnCourseEntity> Courses { get; set; } = new List<UserOnCourseEntity>();
}