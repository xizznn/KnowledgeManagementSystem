using KnowledgeManagementSystem.Core.Entities;

namespace KnowledgeManagementSystem.Core.Interfaces.IServices
{
    public interface IUserOnCourseService
    {
        Task<IEnumerable<UserOnCourseEntity>> GetAllUserCourses();
        Task<bool> AddUserToCourse(UserOnCourseEntity userCourse);
        Task<bool> RemoveUserFromCourse(int userId, int courseId);
        Task<bool> IsUserOnCourse(int userId, int courseId);
        Task<IEnumerable<UserOnCourseEntity>> GetUserCourses(int userId);
    }
}