using KnowledgeManagementSystem.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.Core.Interfaces
{
    public interface IQuestionRepository : IGenericRepository<QuestionEntity>
    {
        Task<IEnumerable<QuestionEntity>> GetByTextAsync(string text);
        Task<IEnumerable<QuestionEntity>> GetByAnswerAsync(string answer);
    }
}
