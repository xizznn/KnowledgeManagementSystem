namespace KnowledgeManagementSystem.Core.Entities
{
    public class TestEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime AddedAt { get; set; }
        public DateTime? EditedAt { get; set; }
        public int? CourseId { get; set; }
        public CourseEntity? Course { get; set; }
        public ICollection<QuestionInTestEntity> Questions { get; set; } = new List<QuestionInTestEntity>();
        public ICollection<TestInCourseEntity> Courses { get; set; } = new List<TestInCourseEntity>();
    }
}