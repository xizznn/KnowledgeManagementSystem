using KnowledgeManagementSystem.Core.DTOs;
using KnowledgeManagementSystem.Core.Entities;
using KnowledgeManagementSystem.Core.Interfaces;
using KnowledgeManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.Infrastructure.Repositories
{
    public class TestRepository : GenericRepository<TestEntity>, ITestRepository
    {
        public TestRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<TestEntity>> GetByTitleAsync(string title)
            => await _context.Tests
                              .Where(t => t.Title.Contains(title))
                              .ToListAsync();

        public async Task<IEnumerable<TestEntity>> GetByCourseAsync(int courseId)
            => await _context.Tests
                              .Where(t => t.CourseId == courseId)
                              .ToListAsync();
        public async Task<PagedResponse<TestEntity>> GetPagedAsync(int pageNumber, int pageSize, string? searchTerm = null)
        {
            var query = _context.Tests.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(t => t.Title.Contains(searchTerm));
            }

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderBy(t => t.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponse<TestEntity>
            {
                Data = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}
