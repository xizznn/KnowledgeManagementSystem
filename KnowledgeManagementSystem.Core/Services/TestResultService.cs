using AutoMapper;
using KnowledgeManagementSystem.Core.DTOs;
using KnowledgeManagementSystem.Core.Entities;
using KnowledgeManagementSystem.Core.Interfaces;
using KnowledgeManagementSystem.Core.Interfaces.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.Core.Services
{
    public class TestResultService : ITestResultService
    {
        private readonly ITestResultRepository _repo;
        private readonly IMapper _mapper;

        public TestResultService(ITestResultRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TestResultDto>> GetByUser(int userId)
        {
            var results = await _repo.GetByUserAsync(userId);
            return _mapper.Map<IEnumerable<TestResultDto>>(results);
        }

        public async Task<IEnumerable<TestResultDto>> GetByTest(int testId)
        {
            var results = await _repo.GetByTestAsync(testId);
            return _mapper.Map<IEnumerable<TestResultDto>>(results);
        }

        public async Task<IEnumerable<TestResultDto>> GetPendingResults()
        {
            var results = await _repo.GetPendingResultsAsync();
            return _mapper.Map<IEnumerable<TestResultDto>>(results);
        }

        public async Task<int> Add(CreateTestResultDto dto)
        {
            try
            {
                if (dto.UserId <= 0 || dto.TestId <= 0)
                    throw new ArgumentException("Invalid UserId or TestId");

                var entity = new TestResultEntity
                {
                    UserId = dto.UserId,
                    TestId = dto.TestId,
                    Score = null,
                    DateTaken = DateTime.UtcNow
                };

                await _repo.AddAsync(entity);
                await _repo.SaveChangesAsync();

                return entity.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding test result: {ex}");
                throw;
            }
        }

        public async Task<bool> UpdateScore(int id, int score)
        {
            return await _repo.UpdateScoreAsync(id, score);
        }
    }
}