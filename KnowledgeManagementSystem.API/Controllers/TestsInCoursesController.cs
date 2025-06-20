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
    public class TestsInCoursesController : ControllerBase
    {
        private readonly ITestInCourseService _testCourseService;
        private readonly IMapper _mapper;

        public TestsInCoursesController(ITestInCourseService testCourseService, IMapper mapper)
        {
            _testCourseService = testCourseService;
            _mapper = mapper;
        }

        [HttpGet("course/{courseId}")]
        [SwaggerOperation(Summary = "Получить тесты курса")]
        public async Task<ActionResult<IEnumerable<TestInCourseDto>>> GetCourseTests(int courseId)
        {
            var entities = await _testCourseService.GetTestsByCourse(courseId);
            var dtos = _mapper.Map<IEnumerable<TestInCourseDto>>(entities);
            return Ok(dtos);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Добавить тест в курс")]
        public async Task<IActionResult> AddTestToCourse([FromBody] CreateTestInCourseDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = _mapper.Map<TestInCourseEntity>(dto);
            var result = await _testCourseService.AddTestToCourse(entity);

            return result ? Ok() : BadRequest();
        }

        [HttpDelete("{testId}/{courseId}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Удалить тест из курса")]
        public async Task<IActionResult> RemoveTestFromCourse(int testId, int courseId)
        {
            var result = await _testCourseService.RemoveTestFromCourse(testId, courseId);
            return result ? NoContent() : NotFound();
        }
    }
}
