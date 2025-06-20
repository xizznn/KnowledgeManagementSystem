using KnowledgeManagementSystem.Core.Entities;
using KnowledgeManagementSystem.Core.Interfaces;
using KnowledgeManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeManagementSystem.Infrastructure.Repositories
{
    public class TestResultRepository : GenericRepository<TestResultEntity>, ITestResultRepository
    {
        public TestResultRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<TestResultEntity>> GetByUserAsync(int userId)
        {
            return await _context.TestResults
                                 .Include(r => r.Test)
                                 .Include(r => r.User)
                                 .Where(r => r.UserId == userId)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<TestResultEntity>> GetByTestAsync(int testId)
        {
            return await _context.TestResults
                                 .Include(r => r.Test)
                                 .Include(r => r.User)
                                 .Where(r => r.TestId == testId)
                                 .ToListAsync();
        }
        public async Task<IEnumerable<TestResultEntity>> GetPendingResultsAsync()
        {
            return await _context.TestResults
                .Include(r => r.Test)
                .Include(r => r.User)
                .Where(r => r.Score == null)
                .ToListAsync();
        }

        public async Task<bool> UpdateScoreAsync(int id, int score)
        {
            var result = await _context.TestResults.FindAsync(id);
            if (result == null)
                return false;

            result.Score = score;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
