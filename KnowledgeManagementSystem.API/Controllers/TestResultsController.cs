using KnowledgeManagementSystem.Core.DTOs;
using KnowledgeManagementSystem.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestResultsController : ControllerBase
    {
        private readonly ITestResultService _service;

        public TestResultsController(ITestResultService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize]
        [SwaggerOperation(Summary = "Отправить результаты теста на проверку")]
        public async Task<IActionResult> SubmitResult([FromBody] CreateTestResultDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest("Invalid data");

                if (dto.Score != null)
                    return BadRequest("Score must be null when submitting test");

                var id = await _service.Add(dto);
                return Ok(new
                {
                    Id = id,
                    Message = "Test result submitted for review",
                    UserId = dto.UserId,
                    TestId = dto.TestId
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Internal server error",
                    Error = ex.Message
                });
            }
        }

        [HttpPut("{id}/score")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Обновить оценку за тест (только для админов)")]
        public async Task<IActionResult> UpdateScore(int id, [FromBody] UpdateScoreDto dto)
        {
            if (dto.Score < 0 || dto.Score > 100)
                return BadRequest("Число должно быть от 0 до 100");

            var success = await _service.UpdateScore(id, dto.Score);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpGet("user/{userId}")]
        [Authorize]
        [SwaggerOperation(Summary = "Получить результаты тестов пользователя")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var results = await _service.GetByUser(userId);
            return Ok(results);
        }

        [HttpGet("test/{testId}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Получить результаты теста (только для админов)")]
        public async Task<IActionResult> GetByTest(int testId)
        {
            var results = await _service.GetByTest(testId);
            return Ok(results);
        }

        [HttpGet("pending")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Получить результаты тестов, ожидающие проверки (только для админов)")]
        public async Task<IActionResult> GetPendingResults()
        {
            var results = await _service.GetPendingResults();
            return Ok(results);
        }
    }
}
