using AutoMapper;
using KnowledgeManagementSystem.Core.DTOs;
using KnowledgeManagementSystem.Core.Entities;
using KnowledgeManagementSystem.Core.Interfaces;
using KnowledgeManagementSystem.Core.Interfaces.IServices;

namespace KnowledgeManagementSystem.Core.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CourseService(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<CourseDto>> GetAll()
        {
            var courses = await _courseRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CourseDto>>(courses);
        }

        public async Task<CourseDto?> GetCourse(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            return course == null ? null : _mapper.Map<CourseDto>(course);
        }

        public async Task<int> CreateCourse(CreateCourseDto dto)
        {
            var course = _mapper.Map<CourseEntity>(dto);
            course.AddedAt = DateTime.UtcNow;
            await _courseRepository.AddAsync(course);
            return course.Id;
        }

        public async Task<bool> UpdateCourse(UpdateCourseDto dto)
        {
            var existing = await _courseRepository.GetByIdAsync(dto.Id);
            if (existing == null)
                return false;

            _mapper.Map(dto, existing);
            existing.EditedAt = DateTime.UtcNow;

            await _courseRepository.UpdateAsync(existing);

            return true;
        }



        public async Task<bool> DeleteCourse(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
                return false;

            await _courseRepository.RemoveAsync(course);
            return true;
        }

        public async Task<IEnumerable<CourseDto>> GetCoursesByAuthor(string author)
        {
            var courses = await _courseRepository.GetByAuthorAsync(author);
            return _mapper.Map<IEnumerable<CourseDto>>(courses);
        }

        public async Task<IEnumerable<CourseDto>> SearchCoursesByTitle(string title)
        {
            var courses = await _courseRepository.SearchByTitleAsync(title);
            return _mapper.Map<IEnumerable<CourseDto>>(courses);
        }
        public async Task<PagedResponse<CourseDto>> GetAll(int pageNumber = 1, int pageSize = 10, string? searchTerm = null)
        {
            var pagedResult = await _courseRepository.GetPagedAsync(pageNumber, pageSize, searchTerm);
            return new PagedResponse<CourseDto>
            {
                Data = _mapper.Map<IEnumerable<CourseDto>>(pagedResult.Data),
                PageNumber = pagedResult.PageNumber,
                PageSize = pagedResult.PageSize,
                TotalCount = pagedResult.TotalCount
            };
        }
    }
}
