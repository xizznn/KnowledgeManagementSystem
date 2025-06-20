using KnowledgeManagementSystem.Core.DTOs;
using KnowledgeManagementSystem.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.Core.Interfaces
{
    public interface ICourseRepository : IGenericRepository<CourseEntity>
    {
        Task<IEnumerable<CourseEntity>> GetByAuthorAsync(string author);
        Task<IEnumerable<CourseEntity>> SearchByTitleAsync(string title);
        Task<PagedResponse<CourseEntity>> GetPagedAsync(int pageNumber, int pageSize, string? searchTerm = null);
    }
}
