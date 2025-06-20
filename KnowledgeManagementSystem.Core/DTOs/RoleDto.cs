using System.ComponentModel.DataAnnotations;

namespace KnowledgeManagementSystem.Core.DTOs
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
    }

    public class CreateRoleDto
    {
        [Required]
        public string Title { get; set; } = null!;
    }

    public class UpdateRoleDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = null!;
    }
}