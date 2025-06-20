namespace KnowledgeManagementSystem.Core.Entities
{
    public class QuestionEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;
        public string Answer { get; set; } = null!;
        public ICollection<QuestionInTestEntity> Tests { get; set; } = new List<QuestionInTestEntity>();
    }
}