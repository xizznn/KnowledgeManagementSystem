using KnowledgeManagementSystem.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.Core.Interfaces.IServices
{
    public interface IFavoriteTestService
    {
        Task<bool> AddToFavoritesTests(int userId, int testId);
        Task<bool> RemoveFromFavoritesTests(int userId, int testId);
        Task<IEnumerable<TestDto>> GetUserFavoritesTests(int userId);
        Task<bool> IsTestInFavoritesTests(int userId, int testId);
    }
}
