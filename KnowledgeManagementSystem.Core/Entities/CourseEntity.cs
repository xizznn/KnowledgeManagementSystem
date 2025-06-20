namespace KnowledgeManagementSystem.Core.Entities
{
    public class CourseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string UserAuthor { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime AddedAt { get; set; }
        public DateTime? EditedAt { get; set; }
        public ICollection<TestInCourseEntity> Tests { get; set; } = new List<TestInCourseEntity>();
        public ICollection<UserOnCourseEntity> Users { get; set; } = new List<UserOnCourseEntity>();
    }
}