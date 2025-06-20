using KnowledgeManagementSystem.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.Core.Interfaces
{
    public interface ITestResultRepository : IGenericRepository<TestResultEntity>
    {
        Task<IEnumerable<TestResultEntity>> GetByUserAsync(int userId);
        Task<IEnumerable<TestResultEntity>> GetByTestAsync(int testId);
        Task<IEnumerable<TestResultEntity>> GetPendingResultsAsync();
        Task<bool> UpdateScoreAsync(int id, int score);
    }
}
