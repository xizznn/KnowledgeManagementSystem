namespace KnowledgeManagementSystem.Core.Entities
{
    public class TestResultEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public UserEntity User { get; set; } = null!;

        public int TestId { get; set; }
        public TestEntity Test { get; set; } = null!;

        public int? Score { get; set; }
        public DateTime DateTaken { get; set; } = DateTime.UtcNow;
    }
}
