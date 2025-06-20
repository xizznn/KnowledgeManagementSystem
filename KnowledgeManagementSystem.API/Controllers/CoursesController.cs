using AutoMapper;
using KnowledgeManagementSystem.Core.DTOs;
using KnowledgeManagementSystem.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace KnowledgeManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;

        public CoursesController(ICourseService courseService, IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Получить курс по ID")]
        public async Task<IActionResult> GetById(int id)
        {
            var course = await _courseService.GetCourse(id);
            return course != null ? Ok(course) : NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Создать новый курс (только для админов)")]
        public async Task<IActionResult> Create([FromBody] CreateCourseDto createCourseDto)
        {
            var createdCourseId = await _courseService.CreateCourse(createCourseDto);
            var createdCourse = await _courseService.GetCourse(createdCourseId);
            return CreatedAtAction(nameof(GetById), new { id = createdCourseId }, createdCourse);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Обновить существующий курс по ID (только для админов)")]
        public async Task<IActionResult> Update(int id, [FromBody] CourseDto courseDto)
        {
            if (id != courseDto.Id)
                return BadRequest("Id в URL и теле не совпадают");

            try
            {
                var updateCourseDto = _mapper.Map<UpdateCourseDto>(courseDto);
                var updated = await _courseService.UpdateCourse(updateCourseDto);
                if (!updated)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка обновления курса: {ex}");
                return StatusCode(500, $"Ошибка сервера: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Удалить курс по ID (только для админов)")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _courseService.DeleteCourse(id);
            return deleted ? NoContent() : NotFound();
        }

        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Получить список курсов с пагинацией и поиском")]
        public async Task<IActionResult> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null)
        {
            if (pageNumber < 1 || pageSize < 1)
                return BadRequest("Номер страницы и размер должны быть больше 0");

            var result = await _courseService.GetAll(pageNumber, pageSize, searchTerm);
            return Ok(result);
        }
    }
}
