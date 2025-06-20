namespace KnowledgeManagementSystem.Core.DTOs
{
    public class TestDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime AddedAt { get; set; }
        public DateTime? EditedAt { get; set; }
        public List<string>? CourseTitles { get; set; }
    }

    public class CreateTestDto
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int? CourseId { get; set; }
    }

    public class UpdateTestDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int? CourseId { get; set; }
    }
}