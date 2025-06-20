using KnowledgeManagementSystem.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.Core.Interfaces.IServices
{
    public interface ICourseService
    {
        Task<PagedResponse<CourseDto>> GetAll(int pageNumber = 1, int pageSize = 10, string? searchTerm = null);
        Task<CourseDto?> GetCourse(int id);
        Task<int> CreateCourse(CreateCourseDto course);
        Task<bool> UpdateCourse(UpdateCourseDto course);
        Task<bool> DeleteCourse(int id);
        Task<IEnumerable<CourseDto>> GetCoursesByAuthor(string author);
        Task<IEnumerable<CourseDto>> SearchCoursesByTitle(string title);
    }
}