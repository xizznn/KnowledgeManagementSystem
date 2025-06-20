namespace KnowledgeManagementSystem.Core.DTOs
{
    public class FavoriteTestDto
    {
        public int UserId { get; set; }
        public int TestId { get; set; }
        public DateTime AddedDate { get; set; }
    }

    public class CreateFavoriteTestDto
    {
        public int UserId { get; set; }
        public int TestId { get; set; }
    }
}
