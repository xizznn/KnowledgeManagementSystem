using KnowledgeManagementSystem.Core.Entities;

namespace KnowledgeManagementSystem.Core.Interfaces
{
    public interface IUserOnCourseRepository : IGenericRepository<UserOnCourseEntity>
    {
        Task<bool> IsUserOnCourseAsync(int userId, int courseId);
        Task<IEnumerable<UserOnCourseEntity>> GetUserCoursesAsync(int userId);
    }
}