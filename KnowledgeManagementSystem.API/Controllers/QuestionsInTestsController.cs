using AutoMapper;
using KnowledgeManagementSystem.Core.DTOs;
using KnowledgeManagementSystem.Core.Entities;
using KnowledgeManagementSystem.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionsInTestsController : ControllerBase
    {
        private readonly IQuestionInTestService _questionTestService;
        private readonly IMapper _mapper;

        public QuestionsInTestsController(IQuestionInTestService questionTestService, IMapper mapper)
        {
            _questionTestService = questionTestService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Добавить вопрос в тест")]
        public async Task<IActionResult> AddQuestionToTest([FromBody] CreateQuestionInTestDto questionTestDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var questionTestEntity = _mapper.Map<QuestionInTestEntity>(questionTestDto);
            var result = await _questionTestService.AddQuestionToTest(questionTestEntity);

            return result ? Ok() : BadRequest();
        }

        [HttpDelete("{questionId:int}/{testId:int}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Удалить вопрос из теста")]
        public async Task<IActionResult> RemoveQuestionFromTest(int questionId, int testId)
        {
            var result = await _questionTestService.RemoveQuestionFromTest(questionId, testId);
            return result ? NoContent() : NotFound();
        }

        [HttpGet("test/{testId:int}")]
        [SwaggerOperation(Summary = "Получить все вопросы из теста")]
        public async Task<ActionResult<IEnumerable<QuestionInTestDto>>> GetTestQuestions(int testId)
        {
            var questions = await _questionTestService.GetQuestionsByTest(testId);
            var dtos = _mapper.Map<IEnumerable<QuestionInTestDto>>(questions);
            return Ok(dtos);
        }
    }
}
