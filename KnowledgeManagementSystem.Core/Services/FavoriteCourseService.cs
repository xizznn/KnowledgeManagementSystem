using AutoMapper;
using KnowledgeManagementSystem.Core.DTOs;
using KnowledgeManagementSystem.Core.Entities;
using KnowledgeManagementSystem.Core.Interfaces;
using KnowledgeManagementSystem.Core.Interfaces.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.Core.Services
{
    public class FavoriteCourseService : IFavoriteCourseService
    {
        private readonly IFavoriteCourseRepository _favoriteCourseRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public FavoriteCourseService(IFavoriteCourseRepository favoriteCourseRepository,
                             ICourseRepository courseRepository,
                             IMapper mapper)
        {
            _favoriteCourseRepository = favoriteCourseRepository;
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddToFavoritesCourses(int userId, int courseId)
        {
            var favorite = new FavoriteCourseEntity
            {
                UserId = userId,
                CourseId = courseId,
                AddedDate = DateTime.UtcNow
            };
            await _favoriteCourseRepository.AddAsync(favorite);
            return true;
        }

        public async Task<bool> RemoveFromFavoritesCourses(int userId, int courseId)
        {
            var favorite = await _favoriteCourseRepository.GetByUserAndCourseAsync(userId, courseId);
            if (favorite == null) return false;

            await _favoriteCourseRepository.RemoveAsync(favorite);
            return true;
        }

        public async Task<IEnumerable<CourseDto>> GetUserFavoritesCourses(int userId)
        {
            var favorites = await _favoriteCourseRepository.GetByUserIdAsync(userId);
            var courseIds = favorites.Select(f => f.CourseId).ToList();
            var courses = await _courseRepository.GetByIdsAsync(courseIds);
            return _mapper.Map<IEnumerable<CourseDto>>(courses);
        }

        public async Task<bool> IsCourseInFavoritesCourses(int userId, int courseId)
        {
            var favorite = await _favoriteCourseRepository.GetByUserAndCourseAsync(userId, courseId);
            return favorite != null;
        }
    }
}
