namespace KnowledgeManagementSystem.Core.Entities
{
    public class RoleEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
    }
}