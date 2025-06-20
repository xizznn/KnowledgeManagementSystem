using KnowledgeManagementSystem.Core.Entities;
using KnowledgeManagementSystem.Core.Interfaces;
using KnowledgeManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeManagementSystem.Infrastructure.Repositories
{
    public class QuestionInTestRepository : GenericRepository<QuestionInTestEntity>, IQuestionInTestRepository
    {
        public QuestionInTestRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<QuestionInTestEntity>> GetQuestionsByTestAsync(int testId)
        {
            return await _context.QuestionsInTests
                .Include(qt => qt.Question)
                .Where(qt => qt.TestId == testId)
                .ToListAsync();
        }

        public async Task<bool> IsQuestionInTestAsync(int questionId, int testId)
        {
            return await _context.QuestionsInTests
                .AnyAsync(qt => qt.QuestionId == questionId && qt.TestId == testId);
        }
    }
}