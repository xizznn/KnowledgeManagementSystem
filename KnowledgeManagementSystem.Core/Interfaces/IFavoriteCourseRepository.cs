using KnowledgeManagementSystem.Core.Entities;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.Core.Interfaces
{
    public interface IFavoriteCourseRepository : IGenericRepository<FavoriteCourseEntity>
    {
        Task<FavoriteCourseEntity?> GetByUserAndCourseAsync(int userId, int courseId);
        Task<IEnumerable<FavoriteCourseEntity>> GetByUserIdAsync(int userId);
    }
}