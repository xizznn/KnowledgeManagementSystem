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
    public class CourseRepository : GenericRepository<CourseEntity>, ICourseRepository
    {

        public CourseRepository(ApplicationDbContext context)
            : base(context) { }

        public async Task<IEnumerable<CourseEntity>> GetByAuthorAsync(string author)
            => await _context.Courses
                              .Where(c => c.UserAuthor == author)
                              .ToListAsync();

        public async Task<IEnumerable<CourseEntity>> SearchByTitleAsync(string title)
            => await _context.Courses
                              .Where(c => c.Title.Contains(title))
                              .ToListAsync();
        public async Task<PagedResponse<CourseEntity>> GetPagedAsync(int pageNumber, int pageSize, string? searchTerm = null)
        {
            var query = _context.Courses.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(c => c.Title.Contains(searchTerm));
            }

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderBy(c => c.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponse<CourseEntity>
            {
                Data = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}
