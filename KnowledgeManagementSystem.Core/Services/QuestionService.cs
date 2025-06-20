using AutoMapper;
using KnowledgeManagementSystem.Core.DTOs;
using KnowledgeManagementSystem.Core.Entities;
using KnowledgeManagementSystem.Core.Interfaces;
using KnowledgeManagementSystem.Core.Interfaces.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.Infrastructure.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;

        public QuestionService(IQuestionRepository questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<QuestionDto>> GetAllQuestions()
        {
            var entities = await _questionRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<QuestionDto>>(entities);
        }

        public async Task<QuestionDto?> GetQuestion(int id)
        {
            var entity = await _questionRepository.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<QuestionDto>(entity);
        }

        public async Task<int> AddQuestion(CreateQuestionDto questionDto)
        {
            var entity = _mapper.Map<QuestionEntity>(questionDto);
            await _questionRepository.AddAsync(entity);
            return entity.Id;
        }

        public async Task<bool> UpdateQuestion(UpdateQuestionDto questionDto)
        {
            var entity = await _questionRepository.GetByIdAsync(questionDto.Id);
            if (entity == null) return false;
            _mapper.Map(questionDto, entity);
            await _questionRepository.UpdateAsync(entity);
            return true;
        }

        public async Task<bool> DeleteQuestion(int id)
        {
            var entity = await _questionRepository.GetByIdAsync(id);
            if (entity == null) return false;
            await _questionRepository.RemoveAsync(entity);
            return true;
        }

        public async Task<IEnumerable<QuestionDto>> GetQuestionsByText(string text)
        {
            var entities = await _questionRepository.GetByTextAsync(text);
            return _mapper.Map<IEnumerable<QuestionDto>>(entities);
        }
    }
}
