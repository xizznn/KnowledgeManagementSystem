using KnowledgeManagementSystem.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.Core.Interfaces.IServices
{
    public interface IFavoriteCourseService
    {
        Task<bool> AddToFavoritesCourses(int userId, int courseId);
        Task<bool> RemoveFromFavoritesCourses(int userId, int courseId);
        Task<IEnumerable<CourseDto>> GetUserFavoritesCourses(int userId);
        Task<bool> IsCourseInFavoritesCourses(int userId, int courseId);
    }
}
