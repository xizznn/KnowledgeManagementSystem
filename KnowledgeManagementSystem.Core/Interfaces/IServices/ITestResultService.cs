using KnowledgeManagementSystem.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.Core.Interfaces.IServices
{
    public interface ITestResultService
    {
        Task<IEnumerable<TestResultDto>> GetByUser(int userId);
        Task<IEnumerable<TestResultDto>> GetByTest(int testId);
        Task<IEnumerable<TestResultDto>> GetPendingResults();
        Task<int> Add(CreateTestResultDto dto);
        Task<bool> UpdateScore(int id, int score);
    }
}