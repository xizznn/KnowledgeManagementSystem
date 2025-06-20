using KnowledgeManagementSystem.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.Core.Interfaces
{
    public interface IFavoriteTestRepository : IGenericRepository<FavoriteTestEntity>
    {
        Task<FavoriteTestEntity?> GetByUserAndTestAsync(int userId, int testId);
        Task<IEnumerable<FavoriteTestEntity>> GetByUserIdAsync(int userId);
    }
}
