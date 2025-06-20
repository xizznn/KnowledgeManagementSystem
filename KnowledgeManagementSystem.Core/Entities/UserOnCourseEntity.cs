namespace KnowledgeManagementSystem.Core.Entities
{
    public class UserOnCourseEntity
    {
        public int UserId { get; set; }
        public UserEntity User { get; set; } = null!;
        public int CourseId { get; set; }
        public CourseEntity Course { get; set; } = null!;
    }
}