namespace KnowledgeManagementSystem.Core.DTOs
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;
        public string Answer { get; set; } = null!;
    }

    public class CreateQuestionDto
    {
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;
        public string Answer { get; set; } = null!;
    }

    public class UpdateQuestionDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;
        public string Answer { get; set; } = null!;
    }
}