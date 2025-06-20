using AutoMapper;
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
    [Authorize]
    public class FavoritesCoursesController : ControllerBase
    {
        private readonly IFavoriteCourseService _favoriteService;
        private readonly IMapper _mapper;

        public FavoritesCoursesController(IFavoriteCourseService favoriteService, IMapper mapper)
        {
            _favoriteService = favoriteService;
            _mapper = mapper;
        }

        private int GetUserId()
        {
            var userIdClaim = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Получить избранные курсы пользователя")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetUserFavorites()
        {
            var userId = GetUserId();
            if (userId == 0)
                return Unauthorized();

            var favorites = await _favoriteService.GetUserFavoritesCourses(userId);
            return Ok(_mapper.Map<IEnumerable<CourseDto>>(favorites));
        }

        [HttpPost("{courseId}")]
        [SwaggerOperation(Summary = "Добавить курс в избранное")]
        public async Task<IActionResult> AddToFavorites(int courseId)
        {
            var userId = GetUserId();
            if (userId == 0)
                return Unauthorized();

            var result = await _favoriteService.AddToFavoritesCourses(userId, courseId);

            if (!result)
                return BadRequest("Не удалось добавить курс в избранное");

            return Ok();
        }

        [HttpDelete("{courseId}")]
        [SwaggerOperation(Summary = "Удалить курс из избранного")]
        public async Task<IActionResult> RemoveFromFavorites(int courseId)
        {
            var userId = GetUserId();
            if (userId == 0)
                return Unauthorized();

            var result = await _favoriteService.RemoveFromFavoritesCourses(userId, courseId);

            if (!result)
                return BadRequest("Не удалось удалить курс из избранного");

            return NoContent();
        }

        [HttpGet("check/{courseId}")]
        [SwaggerOperation(Summary = "Проверить, есть ли курс в избранном")]
        public async Task<ActionResult<bool>> IsCourseInFavorites(int courseId)
        {
            var userId = GetUserId();
            if (userId == 0)
                return Unauthorized();

            var result = await _favoriteService.IsCourseInFavoritesCourses(userId, courseId);
            return Ok(result);
        }
    }
}
