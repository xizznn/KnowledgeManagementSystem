using KnowledgeManagementSystem.Core.Entities;
using KnowledgeManagementSystem.Core.Interfaces;
using KnowledgeManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeManagementSystem.Infrastructure.Repositories
{
    public class TestInCourseRepository : GenericRepository<TestInCourseEntity>, ITestInCourseRepository
    {
        public TestInCourseRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<TestInCourseEntity>> GetTestsByCourseAsync(int courseId)
        {
            return await _context.TestsInCourses
                .Include(tc => tc.Test)
                .Where(tc => tc.CourseId == courseId)
                .ToListAsync();
        }

        public async Task<bool> IsTestInCourseAsync(int testId, int courseId)
        {
            return await _context.TestsInCourses
                .AnyAsync(tc => tc.TestId == testId && tc.CourseId == courseId);
        }
    }
}