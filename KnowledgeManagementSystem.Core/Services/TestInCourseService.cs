using KnowledgeManagementSystem.Core.Entities;
using KnowledgeManagementSystem.Core.Interfaces;
using KnowledgeManagementSystem.Core.Interfaces.IServices;

namespace KnowledgeManagementSystem.Core.Services
{
    public class TestInCourseService : ITestInCourseService
    {
        private readonly ITestInCourseRepository _testCourseRepository;

        public TestInCourseService(ITestInCourseRepository testCourseRepository)
        {
            _testCourseRepository = testCourseRepository;
        }

        public async Task<IEnumerable<TestInCourseEntity>> GetAllTestCourseRelations() =>
            await _testCourseRepository.GetAllAsync();

        public async Task<bool> AddTestToCourse(TestInCourseEntity testCourse)
        {
            if (await _testCourseRepository.IsTestInCourseAsync(testCourse.TestId, testCourse.CourseId))
                return false;

            await _testCourseRepository.AddAsync(testCourse);
            return true;
        }

        public async Task<bool> RemoveTestFromCourse(int testId, int courseId)
        {
            var testCourse = (await _testCourseRepository.GetTestsByCourseAsync(courseId))
                .FirstOrDefault(tc => tc.TestId == testId);

            if (testCourse == null) return false;

            await _testCourseRepository.RemoveAsync(testCourse);
            return true;
        }

        public async Task<IEnumerable<TestInCourseEntity>> GetTestsByCourse(int courseId) =>
            await _testCourseRepository.GetTestsByCourseAsync(courseId);

        public async Task<bool> IsTestInCourse(int testId, int courseId) =>
            await _testCourseRepository.IsTestInCourseAsync(testId, courseId);
    }
}