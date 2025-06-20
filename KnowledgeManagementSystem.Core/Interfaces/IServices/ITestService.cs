using KnowledgeManagementSystem.Core.DTOs;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.Core.Interfaces.IServices
{
    public interface ITestService
    {
        Task<PagedResponse<TestDto>> GetAllTests(int pageNumber = 1, int pageSize = 10, string? searchTerm = null);
        Task<TestDto?> GetTest(int id);
        Task<int> AddTest(CreateTestDto testDto);
        Task<bool> UpdateTest(UpdateTestDto testDto);
        Task<bool> DeleteTest(int id);
        Task<IEnumerable<TestDto>> GetTestsByTitle(string title);
        Task<IEnumerable<TestDto>> GetTestsByCourse(int courseId);
    }
}