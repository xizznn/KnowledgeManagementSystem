namespace KnowledgeManagementSystem.Core.Entities
{
    public class FavoriteTestEntity
    {
        public int UserId { get; set; }
        public UserEntity User { get; set; } = null!;

        public int TestId { get; set; }
        public TestEntity Test { get; set; } = null!;

        public DateTime AddedDate { get; set; } = DateTime.UtcNow;
    }
}
