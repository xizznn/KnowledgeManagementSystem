namespace KnowledgeManagementSystem.Core.DTOs
{
    public class UserOnCourseDto
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
    }

    public class CreateUserOnCourseDto
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
    }

}
