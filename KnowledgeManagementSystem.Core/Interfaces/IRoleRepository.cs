using KnowledgeManagementSystem.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.Core.Interfaces
{
    public interface IRoleRepository : IGenericRepository<RoleEntity>
    {
        Task<RoleEntity?> GetByTitleAsync(string title);
    }
}
