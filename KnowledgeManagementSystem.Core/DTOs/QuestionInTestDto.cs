namespace KnowledgeManagementSystem.Core.DTOs
{
    public class QuestionInTestDto
    {
        public int TestId { get; set; }
        public int QuestionId { get; set; }
    }

    public class CreateQuestionInTestDto
    {
        public int TestId { get; set; }
        public int QuestionId { get; set; }
    }
}
