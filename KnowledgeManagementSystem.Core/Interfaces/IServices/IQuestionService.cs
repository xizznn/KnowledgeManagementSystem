using KnowledgeManagementSystem.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.Core.Interfaces.IServices
{
    public interface IQuestionService
    {
        Task<IEnumerable<QuestionDto>> GetAllQuestions();
        Task<QuestionDto?> GetQuestion(int id);
        Task<int> AddQuestion(CreateQuestionDto questionDto);
        Task<bool> UpdateQuestion(UpdateQuestionDto questionDto);
        Task<bool> DeleteQuestion(int id);
        Task<IEnumerable<QuestionDto>> GetQuestionsByText(string text);
    }
}