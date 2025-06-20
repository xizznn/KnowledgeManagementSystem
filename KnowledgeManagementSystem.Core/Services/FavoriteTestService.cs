using AutoMapper;
using KnowledgeManagementSystem.Core.DTOs;
using KnowledgeManagementSystem.Core.Entities;
using KnowledgeManagementSystem.Core.Interfaces;
using KnowledgeManagementSystem.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.Core.Services
{
    public class FavoriteTestService : IFavoriteTestService
    {
        private readonly IFavoriteTestRepository _favoriteRepository;
        private readonly ITestRepository _testRepository;
        private readonly IMapper _mapper;

        public FavoriteTestService(IFavoriteTestRepository favoriteRepository,
                                   ITestRepository testRepository,
                                   IMapper mapper)
        {
            _favoriteRepository = favoriteRepository;
            _testRepository = testRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddToFavoritesTests(int userId, int testId)
        {
            var favorite = new FavoriteTestEntity
            {
                UserId = userId,
                TestId = testId,
                AddedDate = DateTime.UtcNow
            };
            await _favoriteRepository.AddAsync(favorite);
            return true;
        }

        public async Task<bool> RemoveFromFavoritesTests(int userId, int testId)
        {
            var favorite = await _favoriteRepository.GetByUserAndTestAsync(userId, testId);
            if (favorite == null) return false;

            await _favoriteRepository.RemoveAsync(favorite);
            return true;
        }

        public async Task<IEnumerable<TestDto>> GetUserFavoritesTests(int userId)
        {
            var favorites = await _favoriteRepository.GetByUserIdAsync(userId);
            var testIds = favorites.Select(f => f.TestId).ToList();
            var tests = await _testRepository.GetByIdsAsync(testIds);
            return _mapper.Map<IEnumerable<TestDto>>(tests);
        }

        public async Task<bool> IsTestInFavoritesTests(int userId, int testId)
        {
            var favorite = await _favoriteRepository.GetByUserAndTestAsync(userId, testId);
            return favorite != null;
        }
    }
}
