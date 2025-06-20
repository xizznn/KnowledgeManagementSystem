using KnowledgeManagementSystem.Core.Entities;
using KnowledgeManagementSystem.Core.Interfaces;
using KnowledgeManagementSystem.Core.Interfaces.IServices;

namespace KnowledgeManagementSystem.Core.Services
{
    public class QuestionInTestService : IQuestionInTestService
    {
        private readonly IQuestionInTestRepository _questionTestRepository;

        public QuestionInTestService(IQuestionInTestRepository questionTestRepository)
        {
            _questionTestRepository = questionTestRepository;
        }

        public async Task<IEnumerable<QuestionInTestEntity>> GetAllQuestionTestRelations() =>
            await _questionTestRepository.GetAllAsync();

        public async Task<bool> AddQuestionToTest(QuestionInTestEntity questionTest)
        {
            if (await _questionTestRepository.IsQuestionInTestAsync(questionTest.QuestionId, questionTest.TestId))
                return false;

            await _questionTestRepository.AddAsync(questionTest);
            return true;
        }

        public async Task<bool> RemoveQuestionFromTest(int questionId, int testId)
        {
            var questionTest = (await _questionTestRepository.GetQuestionsByTestAsync(testId))
                .FirstOrDefault(qt => qt.QuestionId == questionId);

            if (questionTest == null) return false;

            await _questionTestRepository.RemoveAsync(questionTest);
            return true;
        }

        public async Task<IEnumerable<QuestionInTestEntity>> GetQuestionsByTest(int testId) =>
            await _questionTestRepository.GetQuestionsByTestAsync(testId);

        public async Task<bool> IsQuestionInTest(int questionId, int testId) =>
            await _questionTestRepository.IsQuestionInTestAsync(questionId, testId);
    }
}