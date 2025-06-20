using KnowledgeManagementSystem.Core.Entities;
using KnowledgeManagementSystem.Core.Interfaces;
using KnowledgeManagementSystem.Core.Interfaces.IServices;

namespace KnowledgeManagementSystem.Core.Services
{
    public class UserOnCourseService : IUserOnCourseService
    {
        private readonly IUserOnCourseRepository _userCourseRepository;

        public UserOnCourseService(IUserOnCourseRepository userCourseRepository)
        {
            _userCourseRepository = userCourseRepository;
        }

        public async Task<IEnumerable<UserOnCourseEntity>> GetAllUserCourses() =>
            await _userCourseRepository.GetAllAsync();

        public async Task<bool> AddUserToCourse(UserOnCourseEntity userCourse)
        {
            if (await _userCourseRepository.IsUserOnCourseAsync(userCourse.UserId, userCourse.CourseId))
                return false;

            await _userCourseRepository.AddAsync(userCourse);
            return true;
        }

        public async Task<bool> RemoveUserFromCourse(int userId, int courseId)
        {
            var userCourse = (await _userCourseRepository.GetUserCoursesAsync(userId))
                .FirstOrDefault(uc => uc.CourseId == courseId);

            if (userCourse == null) return false;

            await _userCourseRepository.RemoveAsync(userCourse);
            return true;
        }

        public async Task<bool> IsUserOnCourse(int userId, int courseId) =>
            await _userCourseRepository.IsUserOnCourseAsync(userId, courseId);

        public async Task<IEnumerable<UserOnCourseEntity>> GetUserCourses(int userId) =>
            await _userCourseRepository.GetUserCoursesAsync(userId);
    }
}