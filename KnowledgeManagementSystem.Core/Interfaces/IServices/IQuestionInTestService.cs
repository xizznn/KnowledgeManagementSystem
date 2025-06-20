using KnowledgeManagementSystem.Core.Entities;

namespace KnowledgeManagementSystem.Core.Interfaces.IServices
{
    public interface IQuestionInTestService
    {
        Task<IEnumerable<QuestionInTestEntity>> GetAllQuestionTestRelations();
        Task<bool> AddQuestionToTest(QuestionInTestEntity questionTest);
        Task<bool> RemoveQuestionFromTest(int questionId, int testId);
        Task<IEnumerable<QuestionInTestEntity>> GetQuestionsByTest(int testId);
        Task<bool> IsQuestionInTest(int questionId, int testId);
    }
}