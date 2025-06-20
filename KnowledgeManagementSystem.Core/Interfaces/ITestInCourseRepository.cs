using KnowledgeManagementSystem.Core.Entities;

namespace KnowledgeManagementSystem.Core.Interfaces
{
    public interface ITestInCourseRepository : IGenericRepository<TestInCourseEntity>
    {
        Task<IEnumerable<TestInCourseEntity>> GetTestsByCourseAsync(int courseId);
        Task<bool> IsTestInCourseAsync(int testId, int courseId);
    }
}