namespace KnowledgeManagementSystem.Core.DTOs
{
    public class FavoriteCourseDto
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public DateTime AddedDate { get; set; }
    }

    public class CreateFavoriteDto
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
    }
}