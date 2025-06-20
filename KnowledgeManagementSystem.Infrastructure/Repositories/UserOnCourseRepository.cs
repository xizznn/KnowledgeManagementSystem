using KnowledgeManagementSystem.Core.Entities;
using KnowledgeManagementSystem.Core.Interfaces;
using KnowledgeManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeManagementSystem.Infrastructure.Repositories
{
    public class UserOnCourseRepository : GenericRepository<UserOnCourseEntity>, IUserOnCourseRepository
    {
        public UserOnCourseRepository(ApplicationDbContext context) : base(context) { }

        public async Task<bool> IsUserOnCourseAsync(int userId, int courseId)
        {
            return await _context.UsersOnCourses
                .AnyAsync(uc => uc.UserId == userId && uc.CourseId == courseId);
        }

        public async Task<IEnumerable<UserOnCourseEntity>> GetUserCoursesAsync(int userId)
        {
            return await _context.UsersOnCourses
                .Include(uc => uc.Course)
                .Where(uc => uc.UserId == userId)
                .ToListAsync();
        }
    }
}