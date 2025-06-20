using KnowledgeManagementSystem.Core.DTOs;
using KnowledgeManagementSystem.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using AutoMapper;

namespace KnowledgeManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly ITestService _testService;
        private readonly IMapper _mapper;

        public TestsController(ITestService testService, IMapper mapper)
        {
            _testService = testService;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Получить все тесты с пагинацией")]
        public async Task<ActionResult<PagedResponse<TestDto>>> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null)
        {
            if (pageNumber < 1 || pageSize < 1)
                return BadRequest("Номер страницы и размер должны быть больше 0");

            var result = await _testService.GetAllTests(pageNumber, pageSize, searchTerm);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Получить тест по ID")]
        public async Task<ActionResult<TestDto>> GetById(int id)
        {
            var dto = await _testService.GetTest(id);
            return dto == null ? NotFound() : Ok(dto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Создать новый тест")]
        public async Task<ActionResult<TestDto>> Create([FromBody] CreateTestDto dto)
        {
            var newId = await _testService.AddTest(dto);
            var created = await _testService.GetTest(newId);
            return CreatedAtAction(nameof(GetById), new { id = newId }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] TestDto testDto)
        {
            if (id != testDto.Id)
                return BadRequest("Id в URL и теле не совпадают");

            try
            {
                var updateTestDto = _mapper.Map<UpdateTestDto>(testDto);
                var updated = await _testService.UpdateTest(updateTestDto);
                if (!updated)
                    return NotFound();

                return NoContent();
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Ошибка сервера: {ex.Message}\n{ex.StackTrace}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Удалить тест")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _testService.DeleteTest(id))
                return NotFound();
            return NoContent();
        }
    }
}
