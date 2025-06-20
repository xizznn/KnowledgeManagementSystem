using AutoMapper;
using KnowledgeManagementSystem.Core.DTOs;
using KnowledgeManagementSystem.Core.Entities;
using KnowledgeManagementSystem.Core.Interfaces;
using KnowledgeManagementSystem.Core.Interfaces.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.Core.Services
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;
        private readonly IMapper _mapper;

        public TestService(ITestRepository testRepository, IMapper mapper)
        {
            _testRepository = testRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<TestDto>> GetAllTests(int pageNumber = 1, int pageSize = 10, string? searchTerm = null)
        {
            var pagedResult = await _testRepository.GetPagedAsync(pageNumber, pageSize, searchTerm);
            return new PagedResponse<TestDto>
            {
                Data = _mapper.Map<IEnumerable<TestDto>>(pagedResult.Data),
                PageNumber = pagedResult.PageNumber,
                PageSize = pagedResult.PageSize,
                TotalCount = pagedResult.TotalCount
            };
        }

        public async Task<TestDto?> GetTest(int id)
        {
            var entity = await _testRepository.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<TestDto>(entity);
        }

        public async Task<int> AddTest(CreateTestDto testDto)
        {
            var entity = _mapper.Map<TestEntity>(testDto);
            entity.AddedAt = DateTime.UtcNow;
            await _testRepository.AddAsync(entity);
            return entity.Id;
        }

        public async Task<bool> UpdateTest(UpdateTestDto dto)
        {
            var existing = await _testRepository.GetByIdAsync(dto.Id);
            if (existing == null)
                return false;

            _mapper.Map(dto, existing);
            existing.EditedAt = DateTime.UtcNow;

            await _testRepository.SaveChangesAsync();

            return true;
        }


        public async Task<bool> DeleteTest(int id)
        {
            var entity = await _testRepository.GetByIdAsync(id);
            if (entity == null) return false;
            await _testRepository.RemoveAsync(entity);
            return true;
        }

        public async Task<IEnumerable<TestDto>> GetTestsByTitle(string title)
        {
            var entities = await _testRepository.GetByTitleAsync(title);
            return _mapper.Map<IEnumerable<TestDto>>(entities);
        }

        public async Task<IEnumerable<TestDto>> GetTestsByCourse(int courseId)
        {
            var entities = await _testRepository.GetByCourseAsync(courseId);
            return _mapper.Map<IEnumerable<TestDto>>(entities);
        }
    }
}