using KnowledgeManagementSystem.Core.Entities;

namespace KnowledgeManagementSystem.Core.Interfaces
{
    public interface IQuestionInTestRepository : IGenericRepository<QuestionInTestEntity>
    {
        Task<IEnumerable<QuestionInTestEntity>> GetQuestionsByTestAsync(int testId);
        Task<bool> IsQuestionInTestAsync(int questionId, int testId);
    }
}