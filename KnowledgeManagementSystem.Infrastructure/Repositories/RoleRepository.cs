using KnowledgeManagementSystem.Core.Entities;
using KnowledgeManagementSystem.Core.Interfaces;
using KnowledgeManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.Infrastructure.Repositories
{
    public class RoleRepository : GenericRepository<RoleEntity>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext context) : base(context) { }

        public async Task<RoleEntity?> GetByTitleAsync(string title)
        {
            return await _context.Roles
                                 .FirstOrDefaultAsync(r => r.Title == title);
        }
    }
}
