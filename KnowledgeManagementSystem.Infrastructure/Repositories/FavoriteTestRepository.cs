using KnowledgeManagementSystem.Core.Entities;
using KnowledgeManagementSystem.Core.Interfaces;
using KnowledgeManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.Infrastructure.Repositories
{
    public class FavoriteTestRepository : GenericRepository<FavoriteTestEntity>, IFavoriteTestRepository
    {
        public FavoriteTestRepository(ApplicationDbContext context) : base(context) { }

        public async Task<FavoriteTestEntity?> GetByUserAndTestAsync(int userId, int testId)
        {
            return await _context.FavoriteTests
                .FirstOrDefaultAsync(f => f.UserId == userId && f.TestId == testId);
        }

        public async Task<IEnumerable<FavoriteTestEntity>> GetByUserIdAsync(int userId)
        {
            return await _context.FavoriteTests
                .Include(f => f.Test)
                .Where(f => f.UserId == userId)
                .ToListAsync();
        }
    }
}
