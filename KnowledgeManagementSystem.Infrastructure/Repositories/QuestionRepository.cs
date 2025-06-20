using KnowledgeManagementSystem.Core.Entities;
using KnowledgeManagementSystem.Core.Interfaces;
using KnowledgeManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeManagementSystem.Infrastructure.Repositories
{
    public class QuestionRepository : GenericRepository<QuestionEntity>, IQuestionRepository
    {
        public QuestionRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<QuestionEntity>> GetByAnswerAsync(string answer)
        {
            return await _context.Questions
                .Where(q => q.Answer == answer)
                .ToListAsync();
        }

        public async Task<IEnumerable<QuestionEntity>> GetByTextAsync(string text)
        {
            return await _context.Questions
                .Where(q => q.Text.Contains(text))
                .ToListAsync();
        }
    }
}