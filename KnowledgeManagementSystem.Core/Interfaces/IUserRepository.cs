using KnowledgeManagementSystem.Core.Entities;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.Core.Interfaces
{
    public interface IUserRepository : IGenericRepository<UserEntity>
    {
        Task<UserEntity?> GetByEmailAsync(string email);
        Task<UserEntity?> GetByNameAndSurnameAsync(string name, string surname);
        Task<IEnumerable<UserEntity>> GetUsersByRoleAsync(int roleId);
        Task<IEnumerable<UserEntity>> GetUsersByBirthDateAsync(DateTime date);
        Task<UserEntity?> GetUserWithDetailsAsync(int id);
    }
}
