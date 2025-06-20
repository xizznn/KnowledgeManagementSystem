namespace KnowledgeManagementSystem.Core.DTOs
{
    public class LoginUserDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class ChangePasswordDto
    {
        public required string CurrentPassword { get; set; }
        public required string NewPassword { get; set; }
    }
}