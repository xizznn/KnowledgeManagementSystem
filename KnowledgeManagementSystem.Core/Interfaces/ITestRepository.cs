using KnowledgeManagementSystem.Core.DTOs;
using KnowledgeManagementSystem.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.Core.Interfaces
{
    public interface ITestRepository : IGenericRepository<TestEntity>
    {
        Task<IEnumerable<TestEntity>> GetByTitleAsync(string title);
        Task<IEnumerable<TestEntity>> GetByCourseAsync(int courseId);
        Task<PagedResponse<TestEntity>> GetPagedAsync(int pageNumber, int pageSize, string? searchTerm = null);
    }
}
