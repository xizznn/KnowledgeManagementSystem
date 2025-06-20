namespace KnowledgeManagementSystem.Core.DTOs
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string UserAuthor { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime AddedAt { get; set; }
        public DateTime? EditedAt { get; set; }
    }

    public class CreateCourseDto
    {
        public string Title { get; set; } = null!;
        public string UserAuthor { get; set; } = null!;
        public string? Description { get; set; }
    }

    public class UpdateCourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string UserAuthor { get; set; } = null!;
        public string? Description { get; set; }
    }
}