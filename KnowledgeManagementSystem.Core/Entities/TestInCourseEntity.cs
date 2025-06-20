namespace KnowledgeManagementSystem.Core.Entities
{
    public class TestInCourseEntity
    {
        public int CourseId { get; set; }
        public CourseEntity Course { get; set; } = null!;
        public int TestId { get; set; }
        public TestEntity Test { get; set; } = null!;
    }
}