namespace KnowledgeManagementSystem.Core.DTOs
{
    public class TestResultDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserFullName { get; set; } = string.Empty;
        public int TestId { get; set; }
        public string TestTitle { get; set; } = string.Empty;
        public int? Score { get; set; }
        public DateTime DateTaken { get; set; }
    }

    public class CreateTestResultDto
    {
        public int UserId { get; set; }
        public int TestId { get; set; }
        public int? Score { get; set; }
    }
    public class UpdateScoreDto
    {
        public int Score { get; set; }
    }

}
