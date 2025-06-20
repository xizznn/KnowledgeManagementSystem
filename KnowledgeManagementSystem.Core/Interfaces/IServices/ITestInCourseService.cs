using KnowledgeManagementSystem.Core.Entities;

namespace KnowledgeManagementSystem.Core.Interfaces.IServices
{
    public interface ITestInCourseService
    {
        Task<IEnumerable<TestInCourseEntity>> GetAllTestCourseRelations();
        Task<bool> AddTestToCourse(TestInCourseEntity testCourse);
        Task<bool> RemoveTestFromCourse(int testId, int courseId);
        Task<IEnumerable<TestInCourseEntity>> GetTestsByCourse(int courseId);
        Task<bool> IsTestInCourse(int testId, int courseId);
    }
}