using KnowledgeManagementSystem.Core.Entities;
using KnowledgeManagementSystem.Core.Interfaces;
using KnowledgeManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.Infrastructure.Repositories
{
    public class FavoriteCourseRepository : GenericRepository<FavoriteCourseEntity>, IFavoriteCourseRepository
    {
        public FavoriteCourseRepository(ApplicationDbContext context) : base(context) { }

        public async Task<FavoriteCourseEntity?> GetByUserAndCourseAsync(int userId, int courseId)
        {
            return await _context.FavoriteCourses
                .FirstOrDefaultAsync(f => f.UserId == userId && f.CourseId == courseId);
        }

        public async Task<IEnumerable<FavoriteCourseEntity>> GetByUserIdAsync(int userId)
        {
            return await _context.FavoriteCourses
                .Include(f => f.Course)
                .Where(f => f.UserId == userId)
                .ToListAsync();
        }
    }
}
