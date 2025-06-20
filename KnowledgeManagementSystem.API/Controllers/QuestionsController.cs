using KnowledgeManagementSystem.Core.DTOs;
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
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet, AllowAnonymous]
        [SwaggerOperation(Summary = "Получить все вопросы")]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetAll()
        {
            var questions = await _questionService.GetAllQuestions();
            return Ok(questions);
        }

        [HttpGet("{id}"), AllowAnonymous]
        [SwaggerOperation(Summary = "Получить вопрос по ID")]
        public async Task<ActionResult<QuestionDto>> GetById(int id)
        {
            var question = await _questionService.GetQuestion(id);
            return question == null ? NotFound() : Ok(question);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Создать вопрос")]
        public async Task<ActionResult<QuestionDto>> Create([FromBody] CreateQuestionDto dto)
        {
            var newId = await _questionService.AddQuestion(dto);
            var created = await _questionService.GetQuestion(newId);
            return CreatedAtAction(nameof(GetById), new { id = newId }, created);
        }

        [HttpPut("{id}"), Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Обновить вопрос")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateQuestionDto dto)
        {
            var updated = await _questionService.UpdateQuestion(dto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Удалить вопрос")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _questionService.DeleteQuestion(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
