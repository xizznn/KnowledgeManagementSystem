namespace KnowledgeManagementSystem.Core.Entities
{
    public class QuestionInTestEntity
    {
        public int TestId { get; set; }
        public TestEntity Test { get; set; } = null!;
        public int QuestionId { get; set; }
        public QuestionEntity Question { get; set; } = null!;
    }
}